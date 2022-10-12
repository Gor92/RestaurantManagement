using RestaurantManagement.DAL.Database;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.DAL.Abstraction;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;

namespace RestaurantManagement.DAL.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(RestaurantManagementContext context, IAuthService authService) : base(context, authService)
        {
            DbSet = context.Roles;
        }
    }
}
