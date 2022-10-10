using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.API.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
