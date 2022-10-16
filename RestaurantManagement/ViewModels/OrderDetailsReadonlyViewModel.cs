namespace RestaurantManagement.API.ViewModels
{
    public class OrderDetailsReadonlyViewModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public int RestaurantId { get; set; }
    }
}
