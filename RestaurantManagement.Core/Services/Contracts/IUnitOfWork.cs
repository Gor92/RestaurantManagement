using RestaurantManagement.Core.Repositories.Contracts;

namespace RestaurantManagement.Core.Services.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository OrderRepository { get; }
        public IOrderDetailsRepository OrderDetailsRepository { get; }
        //public IPermissionRepository PermissionRepository { get { return _permissionRepository; } }
        public IProductRepository ProductRepository { get; }
        public IRestaurantRepository RestaurantRepository { get; }
        public IRolePermissionRepository RolePermissionRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public ITableRepository TableRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserRolePermissionRepository UserRolePermissionRepository { get; }
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
