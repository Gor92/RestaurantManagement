namespace RestaurantManagement.Core.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<OrderDetails> OrdersDetails { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Table> Tables { get; set; }

    }
}
