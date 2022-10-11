using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;

namespace RestaurantManagement.DAL.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRolePermissionRepository _userRolePermissionRepository;

        public UnitOfWork(IOrderRepository orderRepository,
                          IOrderDetailsRepository orderDetailsRepository,
                          IPermissionRepository permissionRepository,
                          IProductRepository productRepository,
                          IRestaurantRepository restaurantRepository,
                          IRolePermissionRepository rolePermissionRepository,
                          IRoleRepository roleRepository,
                          ITableRepository tableRepository,
                          IUserRepository userRepository,
                          IUserRolePermissionRepository userRolePermissionRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _permissionRepository = permissionRepository;
            _productRepository = productRepository;
            _restaurantRepository = restaurantRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
            _tableRepository = tableRepository;
            _userRepository = userRepository;
            _userRolePermissionRepository = userRolePermissionRepository;
        }

        public IOrderRepository OrderRepository { get { return _orderRepository; } }
        public IOrderDetailsRepository OrderDetailsRepository { get { return _orderDetailsRepository; } }
        public IPermissionRepository PermissionRepository { get { return _permissionRepository; } }
        public IProductRepository ProductRepository { get { return _productRepository; } }
        public IRestaurantRepository RestaurantRepository { get { return _restaurantRepository; } }
        public IRolePermissionRepository RolePermissionRepository { get { return _rolePermissionRepository; } }
        public IRoleRepository RoleRepository { get { return _roleRepository; } }
        public ITableRepository TableRepository { get { return _tableRepository; } }
        public IUserRepository UserRepository { get { return _userRepository; } }
        public IUserRolePermissionRepository UserRolePermissionRepository { get { return _userRolePermissionRepository; } }
    }
}
