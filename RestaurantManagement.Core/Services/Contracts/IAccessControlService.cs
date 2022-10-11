using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.Core.Services.Contracts
{
    public interface IAccessControlService
    {
        Task ValidateAccessByUserAsync(int userId, IEnumerable<Permission> resourceAccesses, CancellationToken cancellationToken);
        void ValidateAccessAsync(ICollection<Permission> userRolePermissions, IEnumerable<Permission> resourceAccesses);
        Task ValidateAccessAsync(int userId, Type classType, string methodName, CancellationToken cancellationToken);

    }
}
