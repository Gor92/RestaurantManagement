using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.RestaurantIdentification.Services.Contracts;
using RestaurantManagement.RestaurantIdentification.Services.Implementations;

namespace RestaurantManagement.RestaurantIdentification.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddTenantIdentification(this IServiceCollection services)
        {
            services.AddScoped<ITenantInformationResolver, TenantInformationResolver>();
            services.AddScoped<ITenantService, TenantService>();
        }
    }
}
