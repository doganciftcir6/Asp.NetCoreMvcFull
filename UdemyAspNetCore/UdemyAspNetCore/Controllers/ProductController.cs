using Microsoft.AspNetCore.Mvc;

namespace UdemyAspNetCore.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
