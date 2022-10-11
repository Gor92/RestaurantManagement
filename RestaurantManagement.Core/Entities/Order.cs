using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class Order : BaseEntity, IDateMetadata, IAuditMetadata, IRestaurant
    {
        public int TableId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual Table Table { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
