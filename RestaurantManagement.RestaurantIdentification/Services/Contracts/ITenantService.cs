using RestaurantManagement.Core.Models;

namespace RestaurantManagement.RestaurantIdentification.Services.Contracts
{
    public interface ITenantService
    {
        Task<TenantContext?> GetTenantAsync(CancellationToken cancellationToken);

    }
}
