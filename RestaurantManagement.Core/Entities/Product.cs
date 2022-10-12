using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class Product : BaseEntity, IAuditMetadata, IDateMetadata, IRestaurant
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual OrderDetails OrderDetails { get; set; }
        public virtual User CreatedUser { get; set; }
        public virtual User UpdatedUser { get; set; }
    }
}
