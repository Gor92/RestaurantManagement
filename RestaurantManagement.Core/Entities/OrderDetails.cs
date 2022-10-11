using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class OrderDetails : BaseEntity, IDateMetadata, IAuditMetadata, IRestaurant
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public int RestaurantId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
