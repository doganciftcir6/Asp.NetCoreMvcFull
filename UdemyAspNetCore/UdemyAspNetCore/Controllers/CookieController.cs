using Microsoft.AspNetCore.Mvc;
using System;

namespace UdemyAspNetCore.Controllers
{
    public class CookieController : Controller
    {
        public IActionResult Index()
        {
            SetCookie();
            ViewBag.Cookie = GetCookie();
            return View();
        }
        //cookie oluşturmak
        private void SetCookie()
        {
            HttpContext.Response.Cookies.Append("Course", "Asp Net Core",new Microsoft.AspNetCore.Http.CookieOptions {
                Expires=DateTime.Now.AddDays(10),
                HttpOnly=true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
            });
        }
        //cokkie ulaşmak
        private string GetCookie()
        {
            string cookieValue;
            HttpContext.Request.Cookies.TryGetValue("Course", out cookieValue);
            return cookieValue;
        }

        //Response -SET
        //Request -GET
    }
}
