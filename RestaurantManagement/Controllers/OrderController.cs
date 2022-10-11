using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.API.Controllers
{
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("a");
        }
    }
}
