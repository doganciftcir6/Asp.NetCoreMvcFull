using Microsoft.AspNetCore.Mvc;

namespace Udemy.JwtAppFrontEnd.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
