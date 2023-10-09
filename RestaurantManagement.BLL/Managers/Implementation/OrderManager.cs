using System.Linq.Expressions;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.BLL.Managers.Contracts;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.Managers.Implementation
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderBL _orderBl;
        private readonly IOrderDetailsBL _orderDetailsBl;
        private readonly IUnitOfWork _unitOfWork;

        public OrderManager(IOrderBL orderBl, IOrderDetailsBL orderDetailsBl, IUnitOfWork unitOfWork)
        {
            _orderBl = orderBl;
            _orderDetailsBl = orderDetailsBl;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> AddOrder(int userId, Order order, IEnumerable<OrderDetails> orderDetails, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            order.TotalPrice = CalculateOrderSum(orderDetails);
            order.IsPaid = false;

            var insertedOrder = await _orderBl.AddAsync(userId, order, cancellationToken);

            var detailsList = orderDetails.ToList();
            detailsList.ForEach(x => x.OrderId = insertedOrder.Id);

            await _orderDetailsBl.AddAsync(userId, detailsList, cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return order;
        }

        public async Task<Order> AddOrderDetailsToOrder(int userId, int orderId, IEnumerable<OrderDetails> orderDetails, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderBl.GetByIdAsync(userId, orderId, cancellationToken);
                orderDetails.ToList().ForEach(x => x.OrderId = order.Id);
                order.TotalPrice = order.TotalPrice + CalculateOrderSum(orderDetails);

                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                await _orderBl.UpdateAsync(userId, order, cancellationToken);
                await _orderDetailsBl.AddAsync(userId, orderDetails.ToList(), cancellationToken);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return order;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }

        }

        public async Task<IEnumerable<Order>> GetOrders(int userId,Expression<Func<Order, bool>> expression, CancellationToken cancellationToken)
        {
            var orders = await _orderBl.GetAsync(userId,expression, cancellationToken);
            return orders;
        }

        private decimal CalculateOrderSum(IEnumerable<OrderDetails> orderDetails)
        {
            decimal sum = 0;
            foreach (var orderDetail in orderDetails)
            {
                sum += orderDetail.ProductPrice * orderDetail.Quantity;
            }

            return sum;
        }
    }
}
