using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;

namespace RestaurantManagement.DAL.Database
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposedValue;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        //private readonly IPermissionRepository _permissionRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRolePermissionRepository _userRolePermissionRepository;
        private readonly DbContext _dbContext;

        public UnitOfWork(IOrderRepository orderRepository,
                          IOrderDetailsRepository orderDetailsRepository,
                          //IPermissionRepository permissionRepository,
                          IProductRepository productRepository,
                          IRestaurantRepository restaurantRepository,
                          IRolePermissionRepository rolePermissionRepository,
                          IRoleRepository roleRepository,
                          ITableRepository tableRepository,
                          IUserRepository userRepository,
                          IUserRolePermissionRepository userRolePermissionRepository,
                          DbContext dbContext)
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
           // _permissionRepository = permissionRepository;
            _productRepository = productRepository;
            _restaurantRepository = restaurantRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
            _tableRepository = tableRepository;
            _userRepository = userRepository;
            _userRolePermissionRepository = userRolePermissionRepository;
            _dbContext = dbContext;
        }

        public IOrderRepository OrderRepository { get { return _orderRepository; } }
        public IOrderDetailsRepository OrderDetailsRepository { get { return _orderDetailsRepository; } }
        //public IPermissionRepository PermissionRepository { get { return _permissionRepository; } }
        public IProductRepository ProductRepository { get { return _productRepository; } }
        public IRestaurantRepository RestaurantRepository { get { return _restaurantRepository; } }
        public IRolePermissionRepository RolePermissionRepository { get { return _rolePermissionRepository; } }
        public IRoleRepository RoleRepository { get { return _roleRepository; } }
        public ITableRepository TableRepository { get { return _tableRepository; } }
        public IUserRepository UserRepository { get { return _userRepository; } }
        public IUserRolePermissionRepository UserRolePermissionRepository { get { return _userRolePermissionRepository; } }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    this.Dispose();
                }

                _disposedValue = true;
            }
        }
    }
}
