using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAL.Database;
using Microsoft.EntityFrameworkCore.Design;
using RestaurantManagement.RestaurantIdentification.Services.Contracts;

namespace RestaurantManagement.DAL.DataBaseContextFactories
{
    public class RestaurantManagmentContextFactory : IDesignTimeDbContextFactory<RestaurantManagementContext>
    {
        private readonly ITenantService _tenantService;

        public RestaurantManagmentContextFactory(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        public RestaurantManagementContext CreateDbContext(string[] args)
        {
            var tenantContext = _tenantService.GetTenantAsync(CancellationToken.None).Result;
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantManagementContext>();
            optionsBuilder.UseSqlServer(tenantContext.ConnectionString);

            return new RestaurantManagementContext(optionsBuilder.Options);
        }
    }
}
