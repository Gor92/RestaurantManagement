using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.API.ViewModels
{
    public class OrderReadonlyViewModel
    {
        public int TableId { get; set; }
        public int RestaurantId { get; set; }
        public List<OrderDetailsReadonlyViewModel> OrderDetailsReadonlyViewModel { get; set; }
    }
}
