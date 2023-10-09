using System.Linq.Expressions;
using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.BLL.Managers.Contracts
{
    public interface IOrderManager
    {
        Task<Order> AddOrder(int userId, Order order, IEnumerable<OrderDetails> orderDetails,
            CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetOrders(int userId, Expression<Func<Order, bool>> expression, CancellationToken cancellationToken);
        Task<Order> AddOrderDetailsToOrder(int userId, int orderId, IEnumerable<OrderDetails> orderDetails, CancellationToken cancellationToken);
    }
}
