using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
