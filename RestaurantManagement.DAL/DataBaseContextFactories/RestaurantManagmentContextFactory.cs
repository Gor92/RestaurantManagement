using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAL.Database;
using Microsoft.EntityFrameworkCore.Design;

namespace RestaurantManagement.DAL.DataBaseContextFactories
{
    public class RestaurantManagmentContextFactory : IDesignTimeDbContextFactory<RestaurantManagementContext>
    {
        public RestaurantManagementContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantManagementContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Source\\Repos\\Gor92\\RestaurantManagement\\RestaurantManagement.Data\\DatabaseFile\\RestaurantManagement.mdf;Integrated Security=True");

            return new RestaurantManagementContext(optionsBuilder.Options);
        }
    }
}
