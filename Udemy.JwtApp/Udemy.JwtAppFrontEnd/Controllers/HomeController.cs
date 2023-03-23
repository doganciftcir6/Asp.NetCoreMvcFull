using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Udemy.JwtAppFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin,Member")]
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
