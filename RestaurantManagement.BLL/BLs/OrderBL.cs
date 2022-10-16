using RestaurantManagement.Core;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.BLs
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderBL(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> AddAsync(int userId, Order order, CancellationToken cancellationToken)
        {
            try
            {
                var insertedOrder = await _orderRepository.InsertAsync(order, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return insertedOrder;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Order> GetAsync(int userId, int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order is null)
            {
                throw new RestaurantManagementException($"Order not found. Id:{orderId}", ErrorType.NotFound);
            }

            return order;
        }

        public Task UpdateAsync(int userId, Order order, CancellationToken cancellationToken)
        {
             _orderRepository.Update(order, cancellationToken);
             return Task.CompletedTask;
        }
    }
}
