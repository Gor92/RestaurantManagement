using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Attributes;

namespace RestaurantManagement.Core.Services.Contracts.BLs
{
    [AccessControl(ResourceType = ResourceType.Order)]
    public interface IOrderDetailsBL
    {
        [AccessControl(AccessLevel = AccessLevel.Add)]
        Task<List<OrderDetails>> AddAsync(int userId, List<OrderDetails> orderDetails, CancellationToken cancellationToken);
    }
}
