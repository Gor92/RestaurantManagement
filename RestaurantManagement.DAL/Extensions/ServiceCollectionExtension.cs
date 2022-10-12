using System;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<CommonContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
            services.AddScoped<IRestaurantSettingsRepository, RestaurantSettingsRepository>();
            services.AddTenantIdentification();
            
            services.AddDbContext<RestaurantManagementContext>(async (sp, options) =>
            {
                var service = sp.GetRequiredService<ITenantService>();
                var tenantContext = await service.GetTenantAsync(CancellationToken.None);

                options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Source\\Repos\\Gor92\\RestaurantManagement\\RestaurantManagement.Data\\DatabaseFile\\RestaurantManagement.mdf;Integrated Security=True");
               
                var dbContext = sp.GetRequiredService<RestaurantManagementContext>();
                dbContext?.Database.Migrate();
            },
            contextLifetime: ServiceLifetime.Scoped,
            optionsLifetime: ServiceLifetime.Scoped);

       

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
