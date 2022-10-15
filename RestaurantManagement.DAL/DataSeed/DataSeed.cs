using RestaurantManagement.Core;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.DAL.DataSeed
{
    public static class DataSeed
    {
        public static void Execute(ModelBuilder modelBuilder)
        {

            var restaurant = new Restaurant()
            {
                Id = 1,
                Name = "Restaurant 1",
                //RowVersion = Guid.NewGuid(),
            };
            modelBuilder.Entity<Restaurant>().HasData(restaurant);

            var user = new User()
            {
                Id = 1,
                Email = "test@gmail.com",
                FirstName = "Test",
                LastName = "Test",
                MiddleName = "Test",
                Password = "1234567",
                MobilePhoneNumber = "43214324213",
                PhoneNumber = "4324213",
                Login = "test",
                RestaurantId = restaurant.Id,
                //RowVersion = Guid.NewGuid(),

            };

            modelBuilder.Entity<User>().HasData(user);


            modelBuilder.Entity<Table>().HasData(new Table()
            {
                Id = 1,
                RestaurantId = restaurant.Id,
                RestaurantRelatedTableId = 1,
                IsReserved = false,
                //RowVersion = Guid.NewGuid(),

            });

            modelBuilder.Entity<Resource>().HasData(new List<Resource>()
            {
                new Resource()
                {
                    Id=1,
                    Description = "Order",
                    //RowVersion = Guid.NewGuid(),
                    SupportedAccess = AccessLevel.Add,
                    Type = ResourceType.Order
                },
                new Resource()
                {
                    Id = 2,
                    Description = "Order",
                    //RowVersion = Guid.NewGuid(),
                    SupportedAccess = AccessLevel.Delete,
                    Type = ResourceType.Order
                },new Resource()
                {
                    Id = 3,
                    Description = "Order",
                    //RowVersion = Guid.NewGuid(),
                    SupportedAccess = AccessLevel.Read,
                    Type = ResourceType.Order
                },new Resource()
                {
                    Id = 4,
                    Description = "Order",
                    //RowVersion = Guid.NewGuid(),
                    SupportedAccess = AccessLevel.Update,
                    Type = ResourceType.Order
                },
            });

            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = 1,
                //RowVersion = Guid.NewGuid(),
                Name = "SuperAdmin",
                RestaurantId = null,
            });

            modelBuilder.Entity<UserRolePermission>().HasData(new List<UserRolePermission>()
            {
                new UserRolePermission()
                {
                    Id =1,
                    //RowVersion = Guid.NewGuid(),
                    AccessLevel = AccessLevel.Add,
                    RestaurantId = null,
                    RoleId = 1,
                    UserId = 1,
                    ResourceId = 1
                },
                new UserRolePermission()
                {
                    Id =2,
                    //RowVersion = Guid.NewGuid(),
                    AccessLevel = AccessLevel.Update,
                    RestaurantId = null,
                    RoleId = 1,
                    UserId = 1,
                    ResourceId = 2
                },
                new UserRolePermission()
                {
                    Id =3,
                    //RowVersion = Guid.NewGuid(),
                    AccessLevel = AccessLevel.Delete,
                    RestaurantId = null,
                    RoleId = 1,
                    UserId = 1,
                    ResourceId = 3
                },
                new UserRolePermission()
                {
                    Id =4,
                    //RowVersion = Guid.NewGuid(),
                    AccessLevel = AccessLevel.Read,
                    RestaurantId = null,
                    RoleId = 1,
                    UserId = 1,
                    ResourceId = 4
                },
            });

            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product()
                {
                    Id =1,
                    Name ="Product 1",
                    CreateDate = new DateTimeOffset(1997,1,1,1,1,1,TimeSpan.FromSeconds(0)),
                    UpdateDate = new DateTimeOffset(1997,1,1,1,1,1,TimeSpan.FromSeconds(0)),
                    CreatedByUserId = user.Id,
                    UpdatedByUserId= user.Id,
                    Description="drink",
                    Price = 10,
                    //RowVersion = Guid.NewGuid(),
                    RestaurantId = restaurant.Id,

                },
                new Product()
                {
                    Id =2,
                    Name ="Product 2",
                    CreateDate = new DateTimeOffset(1997,1,1,1,1,1,TimeSpan.FromSeconds(0)),
                    UpdateDate = new DateTimeOffset(1997,1,1,1,1,1,TimeSpan.FromSeconds(0)),
                    CreatedByUserId = user.Id,
                    UpdatedByUserId= user.Id,
                    Price = 15,
                    Description = "drink",
                    //RowVersion = Guid.NewGuid(),
                    RestaurantId = restaurant.Id,

                },
                new Product()
                {
                    Id =3,
                    Name ="Product 3",
                    CreateDate = new DateTimeOffset(1997,1,1,1,1,1,TimeSpan.FromSeconds(0)),
                    UpdateDate = new DateTimeOffset(1997,1,1,1,1,1,TimeSpan.FromSeconds(0)),
                    CreatedByUserId = user.Id,
                    UpdatedByUserId= user.Id,
                    Price = 20,
                    Description = "drink",
                    //RowVersion = Guid.NewGuid(),
                    RestaurantId = restaurant.Id,
                },
            });
        }
    }
}
