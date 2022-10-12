using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Models;
using RestaurantManagement.DAL.Database;
using Microsoft.Extensions.Configuration;
using RestaurantManagement.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;
using RestaurantManagement.RestaurantIdentification.Extensions;
using RestaurantManagement.RestaurantIdentification.Services.Contracts;

namespace RestaurantManagement.DAL.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDefaultData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CommonContext>(options => options.UseSqlServer(configuration.GetSection("DefaultConnectionString").Value));
            services.AddTenantIdentification();

            services.AddDbContext<RestaurantManagementContext>(async (sp, options) =>
                {
                    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                    TenantContext tenantContext = null; 
                    if (httpContextAccessor.HttpContext != null)
                    {
                        var service = sp.GetRequiredService<ITenantService>();
                        tenantContext = await service.GetTenantAsync(CancellationToken.None);
                    }

                    options.UseSqlServer(tenantContext?.ConnectionString ?? configuration.GetSection("DefaultConnectionString").Value);
                },
            contextLifetime: ServiceLifetime.Scoped,
            optionsLifetime: ServiceLifetime.Scoped);

            services.AddScoped<IRestaurantSettingsRepository, RestaurantSettingsRepository>();

            services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRolePermissionRepository, UserRolePermissionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
