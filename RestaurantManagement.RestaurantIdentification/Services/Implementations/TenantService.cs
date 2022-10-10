using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Entities;
using System.Security.Cryptography.X509Certificates;
using RestaurantManagement.Core.Repositories.Contracts;
using RestaurantManagement.RestaurantIdentification.Services.Contracts;

namespace RestaurantManagement.RestaurantIdentification.Services.Implementations
{
    public class TenantService : AbstractTenantService
    {
        private readonly ITenantInformationResolver _tenantInformationResolver;
        private readonly IRestaurantSettingsRepository _restaurantSettingsRepository;

        public TenantService(IHttpContextAccessor httpContextAccessor,
                             ITenantInformationResolver tenantInformationResolver,
                             IRestaurantSettingsRepository restaurantSettingsRepository) : base(tenantInformationResolver, httpContextAccessor)
        {
            _tenantInformationResolver = tenantInformationResolver;
            _restaurantSettingsRepository = restaurantSettingsRepository;
        }

        protected override async Task<TenantContext?> GetTenantCoreAsync(Expression<Func<RestaurantSettings, bool>> tenantSelector, CancellationToken cancellationToken)
        {
            var tenantSetting = (await _restaurantSettingsRepository.GetAsync(tenantSelector, cancellationToken)).First();

            return new TenantContext()
            {
                RestaurantId = tenantSetting.Id,
                ConnectionString = tenantSetting.DbConnectionString
            };
        }
    }
}
