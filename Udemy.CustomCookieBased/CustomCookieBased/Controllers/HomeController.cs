using CustomCookieBased.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CustomCookieBased.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CustomCookieBased.Controllers
{
    public class HomeController : Controller
    {
        //context
        private readonly CookieContext _context;

        public HomeController(CookieContext context)
        {
            _context = context;
        }

        //giriş yapmak
        public IActionResult SignIn()
        {
            return View(new UserSignInModel());
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            //check işlemi 
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (user != null)
            {
                //demekki kayıt var.
                //rolü var olanları getir.
                var roles = _context.Roles.Where(x => x.UserRoles.Any(x => x.UserId == user.Id)).Select(x=> x.Definition).ToList();
                var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, model.Username),
};
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                //signin olduktan sonra yönlendirme
                return RedirectToAction("Index");
            }
            //kayıt yoksa
            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Member")]
        public IActionResult Member()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
