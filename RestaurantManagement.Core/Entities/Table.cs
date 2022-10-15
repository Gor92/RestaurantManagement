using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class Table : BaseEntity, IRestaurant
    {
        public int RestaurantRelatedTableId { get; set; }
        public int? RestaurantId { get; set; }
        public bool IsReserved { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
