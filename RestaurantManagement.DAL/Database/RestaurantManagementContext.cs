using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.DAL.Database
{
    public class RestaurantManagementContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRolePermission> UserRolePermissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public RestaurantManagementContext(DbContextOptions<RestaurantManagementContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataSeed.DataSeed.Execute(modelBuilder);

            //Order
            modelBuilder.Entity<Order>().Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();

            modelBuilder.Entity<Order>().HasMany(x => x.OrderDetails)
                                        .WithOne(x => x.Order)
                                        .HasForeignKey(x => x.OrderId)
                                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>().HasOne(x => x.Restaurant)
                                        .WithMany(x => x.Orders)
                                        .HasForeignKey(x => x.RestaurantId)
                                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>().HasOne(x => x.Table)
                                        .WithMany(x => x.Orders)
                                        .HasForeignKey(x => x.TableId)
                                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>().HasOne(x => x.CreatedUser)
                                        .WithMany(x => x.CreatedOrders)
                                        .HasForeignKey(x => x.CreatedByUserId)
                                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>().HasOne(x => x.UpdatedUser)
                                       .WithMany(x => x.UpdatedOrders)
                                       .HasForeignKey(x => x.UpdatedByUserId)
                                       .OnDelete(DeleteBehavior.NoAction);

            //Product
            modelBuilder.Entity<Product>().Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();

            modelBuilder.Entity<Product>().HasOne(x => x.Restaurant)
                                          .WithMany(x => x.Products)
                                          .HasForeignKey(x => x.RestaurantId)
                                          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>().HasOne(x => x.OrderDetails)
                                          .WithOne(x => x.Product)
                                          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>().HasOne(x => x.CreatedUser)
                                          .WithMany(x => x.CreatedProducts)
                                          .HasForeignKey(x => x.CreatedByUserId)
                                          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>().HasOne(x => x.UpdatedUser)
                                          .WithMany(x => x.UpdatedProducts)
                                          .HasForeignKey(x => x.UpdatedByUserId)
                                          .OnDelete(DeleteBehavior.NoAction);

            //Table
            modelBuilder.Entity<Table>().HasKey(x => x.Id);
            modelBuilder.Entity<Table>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Table>().Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();

            modelBuilder.Entity<Table>().HasOne(x => x.Restaurant)
                                        .WithMany(x => x.Tables)
                                        .HasForeignKey(x => x.RestaurantId)
                                        .OnDelete(DeleteBehavior.NoAction);

            //User
            modelBuilder.Entity<User>().Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();

            //Role
            modelBuilder.Entity<Role>().Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();

            ////Permission
            //modelBuilder.Entity<Permission>().HasKey(x => x.Id);
            //modelBuilder.Entity<Permission>().Property(x => x.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Permission>().Property(x => x.RowVersion).IsRowVersion();

            //UserRolePermission
            modelBuilder.Entity<UserRolePermission>().Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    if (!options.IsConfigured)
        //    {
        //        options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Source\\Repos\\Gor92\\RestaurantManagement\\RestaurantManagement.Data\\DatabaseFile\\RestaurantManagement.mdf;Integrated Security=True");
        //    }
        //}
    }
}
