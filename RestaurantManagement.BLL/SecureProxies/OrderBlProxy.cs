using System.Linq.Expressions;
using RestaurantManagement.BLL.BLs;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.SecureProxies
{
    public class OrderBlProxy : IOrderBL
    {
        private readonly IAccessControlService _accessControlService;
        private readonly IOrderBL _orderBl;

        public OrderBlProxy(IOrderBL orderBl, IAccessControlService accessControlService)
        {
            _orderBl = orderBl;
            _accessControlService = accessControlService;
        }
        public async Task<Order> AddAsync(int userId, Order order, CancellationToken cancellationToken)
        {
            await _accessControlService.ValidateAccessAsync(userId, typeof(IOrderBL), nameof(AddAsync), cancellationToken);
            return await _orderBl.AddAsync(userId, order, cancellationToken);
        }

        public async Task<Order> GetByIdAsync(int userId, int orderId, CancellationToken cancellationToken)
        {
            await _accessControlService.ValidateAccessAsync(userId, typeof(IOrderBL), nameof(GetByIdAsync), cancellationToken);
            return await _orderBl.GetByIdAsync(userId, orderId, cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetAsync(int userId, Expression<Func<Order, bool>> expression, CancellationToken cancellationToken)
        {
            await _accessControlService.ValidateAccessAsync(userId, typeof(IOrderBL), nameof(GetAsync), cancellationToken);

            return await _orderBl.GetAsync(userId, expression, cancellationToken);
        }

        public async Task UpdateAsync(int userId, Order order, CancellationToken cancellationToken)
        {
            await _accessControlService.ValidateAccessAsync(userId, typeof(IOrderBL), nameof(UpdateAsync), cancellationToken);
            await _orderBl.UpdateAsync(userId, order, cancellationToken);
        }
    }
}
