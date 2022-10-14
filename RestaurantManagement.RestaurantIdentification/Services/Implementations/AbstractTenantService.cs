using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.RestaurantIdentification.Services.Contracts;

namespace RestaurantManagement.RestaurantIdentification.Services.Implementations
{
    public abstract class AbstractTenantService : ITenantService
    {
        private readonly ITenantInformationResolver _tenantInformationResolver;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AbstractTenantService(ITenantInformationResolver tenantInformationResolver, IHttpContextAccessor httpContextAccessor)
        {
            _tenantInformationResolver = tenantInformationResolver;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TenantContext?> GetTenantAsync(CancellationToken cancellationToken)
        {
            var tenantSelector = _tenantInformationResolver.GetTenantSelector(_httpContextAccessor.HttpContext!);
            return await GetTenantCoreAsync(tenantSelector, cancellationToken);
        }

        protected abstract Task<TenantContext?> GetTenantCoreAsync(Expression<Func<RestaurantSettings, bool>> tenantSelector, CancellationToken cancellationToken);

    }
}
