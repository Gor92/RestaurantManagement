using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core;
using System.Collections.Generic;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Attributes;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.SecureProxies
{
    public class OrderBlProxy : IOrderBL
    {
        private readonly IAccessControlService _accessControlService;
        private readonly IOrderBL _orderBl;

        public OrderBlProxy(IOrderBL orderBL, IAccessControlService accessControlService)
        {
            _orderBl = orderBL;
            _accessControlService = accessControlService;
        }
        public async Task<Order> AddAsync(int userId, Order order, CancellationToken cancellationToken)
        {
            await _accessControlService.ValidateAccessAsync(userId, typeof(IOrderBL), nameof(AddAsync), cancellationToken);
            return await _orderBl.AddAsync(userId, order, cancellationToken);
        }

    }
}
