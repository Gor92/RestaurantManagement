using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.API.Controllers
{
    public class TableController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
