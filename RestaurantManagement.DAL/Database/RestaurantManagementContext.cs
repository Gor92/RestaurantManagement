using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.DAL.Database
{
    public class RestaurantManagementContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<UserRolePermission> UserRolePermission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Order
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>().Property(x => x.RowVersion).IsRowVersion();

            //Product
            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(x => x.RowVersion).IsRowVersion();

            //Table
            modelBuilder.Entity<Table>().HasKey(x => x.Id);
            modelBuilder.Entity<Table>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Table>().Property(x => x.RowVersion).IsRowVersion();

            //User
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(x => x.RowVersion).IsRowVersion();

            //Role
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Role>().Property(x => x.RowVersion).IsRowVersion();

            //Permission
            modelBuilder.Entity<Permission>().HasKey(x => x.Id);
            modelBuilder.Entity<Permission>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Permission>().Property(x => x.RowVersion).IsRowVersion();

            //UserRolePermission
            modelBuilder.Entity<UserRolePermission>().HasKey(x => x.Id);
            modelBuilder.Entity<UserRolePermission>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserRolePermission>().Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
