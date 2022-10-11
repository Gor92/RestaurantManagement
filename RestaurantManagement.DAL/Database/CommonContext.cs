using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.DAL.Database
{
    public class CommonContext : DbContext
    {
        public DbSet<RestaurantSettings> RestaurantSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Order
            modelBuilder.Entity<RestaurantSettings>().HasKey(x => x.Id);
            modelBuilder.Entity<RestaurantSettings>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RestaurantSettings>().Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
