using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.API.Controllers
{
    public class ProductController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
