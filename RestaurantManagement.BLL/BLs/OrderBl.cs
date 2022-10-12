using RestaurantManagement.Core.Entities;
using System.ComponentModel.DataAnnotations;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.BLs
{
    public class OrderBL : IOrderBL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderDetailsBL _orderDetailsBL;

        public OrderBL(IUnitOfWork unitOfWork, IOrderDetailsBL orderDetailsBl)
        {
            _unitOfWork = unitOfWork;
            _orderDetailsBL = orderDetailsBl;
        }
        public async Task<Order> AddAsync(int userId, Order order, IEnumerable<OrderDetails> orderDetails, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                await _orderDetailsBL.AddAsync(userId, orderDetails.ToList(), cancellationToken);

                order.TotalPrice = CalculateOrderSum(orderDetails);
                order.IsPaid = false;

                await _unitOfWork.OrderRepository.InsertAsync(order, cancellationToken);

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
