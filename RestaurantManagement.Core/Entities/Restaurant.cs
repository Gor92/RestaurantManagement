namespace RestaurantManagement.Core.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
