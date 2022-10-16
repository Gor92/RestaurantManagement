using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAL.Database;
using Microsoft.EntityFrameworkCore.Design;

namespace RestaurantManagement.DAL.DataBaseContextFactories
{
    public class CommonContextFactory : IDesignTimeDbContextFactory<CommonContext>
    {
        public CommonContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CommonContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RestaurantManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new CommonContext(optionsBuilder.Options);
        }
    }
}
