﻿using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class Product : BaseEntity, IRestaurant
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
