using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.SecureProxies
{
    public class OrderDetailsBlProxy : IOrderDetailsBL
    {
        private readonly IAccessControlService _accessControlService;
        private readonly IOrderDetailsBL _orderDetailsBl;

        public OrderDetailsBlProxy(IOrderDetailsBL orderDetailsBl, IAccessControlService accessControlService)
        {
            _orderDetailsBl = orderDetailsBl;
            _accessControlService = accessControlService;
        }
        public async Task<List<OrderDetails>> AddAsync(int userId, List<OrderDetails> orderDetails, CancellationToken cancellationToken)
        {
            await _accessControlService.ValidateAccessAsync(userId, typeof(IOrderDetailsBL), nameof(AddAsync), cancellationToken);

            return await _orderDetailsBl.AddAsync(userId, orderDetails, cancellationToken);

        }
    }
}
