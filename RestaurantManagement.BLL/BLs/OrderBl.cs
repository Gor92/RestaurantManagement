using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.BLs
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailsBL _orderDetailsBL;
        private readonly IUnitOfWork _unitOfWork;

        public OrderBL(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IOrderDetailsBL orderDetailsBl)
        {
            _orderRepository = orderRepository;
            _orderDetailsBL = orderDetailsBl;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> AddAsync(int userId, Order order, IEnumerable<OrderDetails> orderDetails, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                await _orderDetailsBL.AddAsync(userId, orderDetails.ToList(), cancellationToken);

                order.TotalPrice = CalculateOrderSum(orderDetails);
                order.IsPaid = false;

                _orderRepository.Insert(order, cancellationToken);

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
