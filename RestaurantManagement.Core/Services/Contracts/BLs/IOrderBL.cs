using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Attributes;

namespace RestaurantManagement.Core.Services.Contracts.BLs
{
    [AccessControl(ResourceType = ResourceType.Order)]
    public interface IOrderBL
    {
        [AccessControl(AccessLevel = AccessLevel.Add)]
        Task<Order> AddAsync(int userId, Order order, IEnumerable<OrderDetails> orderDetails, CancellationToken cancellationToken);
    }
}
