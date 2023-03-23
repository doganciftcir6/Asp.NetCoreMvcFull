using Microsoft.AspNetCore.Mvc;

namespace UdemyAspNetCore.Areas.Member.Controllers
{
    [Area("Member")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
