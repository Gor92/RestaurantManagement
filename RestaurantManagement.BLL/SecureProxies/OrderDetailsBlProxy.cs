using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.BLL.BLs;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.SecureProxies
{
    public class OrderDetailsBlProxy : IOrderDetailsBL
    {
        private readonly IAccessControlService _accessControlService;
        private readonly IOrderDetailsBL _orderDetailsBL;

        public OrderDetailsBlProxy(IOrderDetailsBL orderDetailsBL, IAccessControlService accessControlService)
        {
            _orderDetailsBL = orderDetailsBL;
            _accessControlService = accessControlService;
        }
        public async Task<List<OrderDetails>> AddAsync(int userId, List<OrderDetails> orderDetails, CancellationToken cancellationToken)
        {
            await _accessControlService.ValidateAccessAsync(userId, typeof(IOrderDetailsBL), nameof(AddAsync), cancellationToken);

            return await _orderDetailsBL.AddAsync(userId, orderDetails, cancellationToken);

        }
    }
}
