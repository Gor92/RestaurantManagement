using RestaurantManagement.DAL.Database;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.DAL.Abstraction;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;

namespace RestaurantManagement.DAL.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(RestaurantManagementContext context, IAuthService authService) : base(context, authService)
        {
            DbSet = context.Orders;
        }
    }
}
