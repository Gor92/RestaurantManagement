using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Repositories.Contracts;
using RestaurantManagement.RestaurantIdentification.Services.Contracts;

namespace RestaurantManagement.RestaurantIdentification.Services.Implementations
{
    public class TenantService : AbstractTenantService
    {
        private readonly IRestaurantSettingsRepository _restaurantSettingsRepository;

        public TenantService(IHttpContextAccessor httpContextAccessor,
                             ITenantInformationResolver tenantInformationResolver,
                             IRestaurantSettingsRepository restaurantSettingsRepository) : base(tenantInformationResolver, httpContextAccessor)
        {
            _restaurantSettingsRepository = restaurantSettingsRepository;
        }

        protected override async Task<TenantContext?> GetTenantCoreAsync(Expression<Func<RestaurantSettings, bool>> tenantSelector, CancellationToken cancellationToken)
        {
            var tenantSetting = (await _restaurantSettingsRepository.GetAsync<RestaurantSettings>(tenantSelector, cancellationToken)).FirstOrDefault();
            if(tenantSetting == null)
                return null;
            
            return new TenantContext()
            {
                RestaurantId = tenantSetting.RestaurantId.Value,
                ConnectionString = tenantSetting.DbConnectionString
            };
        }
    }
}
