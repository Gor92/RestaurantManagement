using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.DAL.Database
{
    public class RestaurantManagementContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        //public DbSet<Permission> Permission { get; set; }
        public DbSet<UserRolePermission> UserRolePermission { get; set; }

        public RestaurantManagementContext(DbContextOptions<RestaurantManagementContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Order
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>().Property(x => x.RowVersion).IsRowVersion();

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
                                        .WithMany(x => x.Orders)
                                        .HasForeignKey(x => x.CreatedByUserId)
                                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>().HasOne(x => x.UpdatedUser)
                                       .WithMany(x => x.Orders)
                                       .HasForeignKey(x => x.UpdatedByUserId)
                                       .OnDelete(DeleteBehavior.NoAction);

            //Product
            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(x => x.RowVersion).IsRowVersion();

            modelBuilder.Entity<Product>().HasOne(x => x.Restaurant)
                                          .WithMany(x => x.Products)
                                          .HasForeignKey(x => x.RestaurantId)
                                          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>().HasOne(x => x.OrderDetails)
                                          .WithOne(x => x.Product)
                                          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>().HasOne(x => x.CreatedUser)
                                          .WithMany(x => x.Products)
                                          .HasForeignKey(x => x.CreatedByUserId)
                                          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>().HasOne(x => x.UpdatedUser)
                                          .WithMany(x => x.Products)
                                          .HasForeignKey(x => x.UpdatedByUserId)
                                          .OnDelete(DeleteBehavior.NoAction);

            //Table
            modelBuilder.Entity<Table>().HasKey(x => x.Id);
            modelBuilder.Entity<Table>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Table>().Property(x => x.RowVersion).IsRowVersion();

            modelBuilder.Entity<Table>().HasOne(x => x.Restaurant)
                                        .WithMany(x => x.Tables)
                                        .HasForeignKey(x => x.RestaurantId)
                                        .OnDelete(DeleteBehavior.NoAction);

            //User
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(x => x.RowVersion).IsRowVersion();

            //Role
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Role>().Property(x => x.RowVersion).IsRowVersion();

            ////Permission
            //modelBuilder.Entity<Permission>().HasKey(x => x.Id);
            //modelBuilder.Entity<Permission>().Property(x => x.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Permission>().Property(x => x.RowVersion).IsRowVersion();

            //UserRolePermission
            modelBuilder.Entity<UserRolePermission>().HasKey(x => x.Id);
            modelBuilder.Entity<UserRolePermission>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserRolePermission>().Property(x => x.RowVersion).IsRowVersion();
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
