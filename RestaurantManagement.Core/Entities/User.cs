﻿using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class User : BaseEntity, IDateMetadata, IRestaurant
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDeleted { get; set; }
        public int? RestaurantId { get; set; }
        public virtual ICollection<Order> CreatedOrders { get; set; }
        public virtual ICollection<Order> UpdatedOrders { get; set; }
        public virtual ICollection<OrderDetails> CreatedOrderDetails { get; set; }
        public virtual ICollection<OrderDetails> UpdatedOrderDetails { get; set; }
        public virtual ICollection<Product> CreatedProducts { get; set; }
        public virtual ICollection<Product> UpdatedProducts { get; set; }

    }
}
