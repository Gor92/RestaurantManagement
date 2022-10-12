namespace RestaurantManagement.API.ViewModels;

public class TableReadonlyViewModel
{
    public int Id { get; set; }
    public int RestaurantRelatedId { get; set; }
    public bool IsReserved { get; set; }
}