namespace RestaurantManagement.API.ViewModels
{
    public class OrderDetailsUpdateViewModel
    {
        public int OrderId { get; set; }
        public List<OrderDetailsReadonlyViewModel> OrderDetailsReadonlyViewModels { get; set; }
    }
}
