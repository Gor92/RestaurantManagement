using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAL.Database;
using Microsoft.Extensions.Configuration;
using RestaurantManagement.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
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

                options.UseSqlServer(tenantContext != null
                                                   ? tenantContext.ConnectionString ?? configuration.GetConnectionString("DefaultConnectionString")
                                                   : configuration.GetConnectionString("DefaultConnectionString"));
            });
        }
    }
}
