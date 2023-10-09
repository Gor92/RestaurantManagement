using System.Linq.Expressions;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Attributes;

namespace RestaurantManagement.Core.Services.Contracts.BLs
{
    [AccessControl(ResourceType = ResourceType.Order)]
    public interface IOrderBL
    {
        [AccessControl(AccessLevel = AccessLevel.Add)]
        Task<Order> AddAsync(int userId, Order order, CancellationToken cancellationToken);

        [AccessControl(AccessLevel = AccessLevel.Read)]
        Task<Order> GetByIdAsync(int userId, int orderId, CancellationToken cancellationToken);

        [AccessControl(AccessLevel = AccessLevel.Read)]
        Task<IEnumerable<Order>> GetAsync(int userId, Expression<Func<Order, bool>> expression, CancellationToken cancellationToken);

        [AccessControl(AccessLevel = AccessLevel.Update)]
        Task UpdateAsync(int userId, Order order, CancellationToken cancellationToken);
    }
}
