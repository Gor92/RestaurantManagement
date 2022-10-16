using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.API.Controllers
{
    public class UserController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
