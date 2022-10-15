using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class Order : BaseEntity, IDateMetadata, IAuditMetadata, IRestaurant
    {
        public int TableId { get; set; }
        public int? RestaurantId { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual Table Table { get; set; }
        public virtual User CreatedUser { get; set; }
        public virtual User UpdatedUser { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
