using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Udemy.Identity.Entities;
using Udemy.Identity.Models;

namespace Udemy.Identity.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        //kullanıcı kaydı için userManagerdan faydalanmak
        private readonly UserManager<AppUser> _userManager;
        //kullanıcı giriş için SignInManagerdan faydalanmak
        private readonly SignInManager<AppUser> _signInManager;
        //rol ekleyebilmek için
        private readonly RoleManager<AppRole> _roleManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        //AccessDenied özelleştirmek
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        //Create User
        public IActionResult Create()
        {
            return View(new UserCreateModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Email = model.Email,
                    Gender = model.Gender,
                    UserName = model.UserName
                };
          
                var identityResult = await _userManager.CreateAsync(user, model.Password);
                if (identityResult.Succeeded)
                {
                    //role oluşturmak ve kontrolü
                    var memberRole = await _roleManager.FindByNameAsync("Member");
                    if (memberRole == null)
                    {
                        //role oluştrumak
                        await _roleManager.CreateAsync(new()
                        {
                            Name = "Member",
                            CreatedTime = DateTime.Now
                        });
                    }
                   
                    //role eklemek ilgili kullanıcıyı oluştururken
                    await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Index");
                }
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        //SİGN in user
        public IActionResult SignIn(string returnUrl)
        {
            //girişten sonra url bilgisini alalım
            return View(new UserSignInModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            if (ModelState.IsValid)
            {
                //usurname bilgisini çekmek
                var user = await _userManager.FindByNameAsync(model.UserName);
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
                if (signInResult.Succeeded)
                {
                    //Bu iş başarılı
                    //return url'i ele almak
                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    //role bilgisini çekmek
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("AdminPanel");
                    }
                    else
                    {
                        return RedirectToAction("Panel");
                    }
                }
              
                else if (signInResult.IsLockedOut)
                {
                    //ne zamana kadar kilitlendiğinin bilgisini almak yani locked zamanını ayarlamak.
                    //13.69 - 14.02 minutes
                    var lockOutEnd =  await _userManager.GetLockoutEndDateAsync(user);

                    ModelState.AddModelError("", $"Hesabınız {(lockOutEnd.Value.UtcDateTime-DateTime.UtcNow).Minutes} dk süreyle askıya alınmıştır.");
                }
                else
                {
                    //kaç kere hatalı giriş yapıldığının bilgisini çekelim.
                    var message = string.Empty;
                    
                    if (user != null)
                    {
                        var failedCount = await _userManager.GetAccessFailedCountAsync(user);
                        message = $"{(_userManager.Options.Lockout.MaxFailedAccessAttempts - failedCount)} kez daha girerseniz hesabınız geçici olarak kilitlenecektir";
                    }
                    else
                    {
                        message = "Kullanıcı adı ve şifre hatalı";
                    }
                    ModelState.AddModelError("", message);
                }
               
                

                //else if (signInResult.IsLockedOut)
                //{
                //    //hesap kilitli
                //}
                //else if (signInResult.IsNotAllowed)
                //{
                //    //email veya phonenumber doğrulanmamış
                //}
            }
            return View(model);
        }
        //buraya sadece giriş yapmış kullanıcıların erişmesini istiyorum
        [Authorize]
        public IActionResult GetUserInfo()
        {
            //kullanıcıların datalarına ulaşmak
            var userName = User.Identity.Name;
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            User.IsInRole("Member");

            return View();
        }
        //buraya sadece adminlar girsin
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
        //Buraya sadece memberlar girecek
        [Authorize(Roles = "Member")]
        public IActionResult Panel()
        {
           return View();
        }
        [Authorize(Roles = "Member")]
        public IActionResult MemberPage()
        {
            return View();
        }
        //çıkış yap action
        public async Task <IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
