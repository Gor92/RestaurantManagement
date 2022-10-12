using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
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
                RowVersion = 1,
            };
            modelBuilder.Entity<Restaurant>().HasData(restaurant);

            var user = new User()
            {
                Id = 1,
                Email ="test@gmail.com",
                FirstName="Test",
                LastName = "Test",
                MiddleName="Test",
                Password="1234567",
                MobilePhoneNumber="43214324213",
                PhoneNumber="4324213",
                Login = "test",
                RestaurantId = restaurant.Id,
                RowVersion = 1,

            };

            modelBuilder.Entity<User>().HasData(user);


            modelBuilder.Entity<Table>().HasData(new Table()
            {
                Id = 1,
                RestaurantId = restaurant.Id,
                RestaurantRelatedTableId = 1,
                IsReserved = false,
                RowVersion = 1,

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
                    RowVersion = 1,
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
                    RowVersion = 2,
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
                    RowVersion = 1,
                    RestaurantId = restaurant.Id,
                },
            });
        }
    }
}
