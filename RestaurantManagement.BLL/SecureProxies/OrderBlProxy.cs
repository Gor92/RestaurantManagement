using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.SecureProxies
{
    public class OrderBlProxy : IOrderBL
    {
        private readonly IAccessControlService _accessControlService;
        private readonly IOrderBL _orderBl;

        public OrderBlProxy(IOrderBL orderBl, IAccessControlService accessControlService)
        {
            _orderBl = orderBl;
            _accessControlService = accessControlService;
        }
        public async Task<Order> AddAsync(int userId, Order order, IEnumerable<OrderDetails> orderDetails, CancellationToken cancellationToken)
        {
            await _accessControlService.ValidateAccessAsync(userId, typeof(IOrderBL), nameof(AddAsync), cancellationToken);
            return await _orderBl.AddAsync(userId, order, orderDetails, cancellationToken);
        }

    }
}
