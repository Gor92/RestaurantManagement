using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Attributes;

namespace RestaurantManagement.Core.Services.Contracts.BLs
{
    [AccessControl(ResourceType = ResourceType.Order)]
    public interface IOrderBL
    {
        [AccessControl(AccessLevel = AccessLevel.Add)]
        Task<Order> AddAsync(int userId, Order order, CancellationToken cancellationToken);
    }
}
