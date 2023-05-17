using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Udemy.JwtAppFrontEnd.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPage()
        {
            return View();
        }

        [Authorize(Roles = "Member")]
        public IActionResult MemberPage()
        {
            return View();
        }
    }
}
