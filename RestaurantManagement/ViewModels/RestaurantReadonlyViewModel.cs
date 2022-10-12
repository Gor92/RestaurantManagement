namespace RestaurantManagement.API.ViewModels;

public class RestaurantReadonlyViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TableReadonlyViewModel> Tables { get; set; }
}