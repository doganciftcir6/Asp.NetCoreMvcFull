using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Udemy.AdvertisementApp.Business.Interfaces;
using Udemy.AdvertisementApp.Common.Enums;
using Udemy.AdvertisementApp.Dtos;
using Udemy.AdvertisementApp.UI.Extensions;
using Udemy.AdvertisementApp.UI.Models;

namespace Udemy.AdvertisementApp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _userCreateModelValidator;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public AccountController(IGenderService genderService, IValidator<UserCreateModel> userCreateModelValidator, IAppUserService appUserService, IMapper mapper)
        {
            _genderService = genderService;
            _userCreateModelValidator = userCreateModelValidator;
            _appUserService = appUserService;
            _mapper = mapper;
        }

        public async Task<IActionResult> SignUp()
        {
            //tüm gender bilgilerini çekip modelimizin genders kısmına bunları seleclist olarak eklemek.
            var response = await _genderService.GetAllAsync();
            var model = new UserCreateModel
            {
                Genders = new SelectList(response.Data,"Id","Definition")
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {
            var result = _userCreateModelValidator.Validate(model);
            if (result.IsValid)
            {
                //kullanıcıyı db ye kayıt etmek rolü ile birlikte. CreateWithRoleAsync(dto,2); şeklinde yazmak mantıksız olur sonraki develp'ler için bunun yerine enum oluşturup kullanabiliriz. Bu olay genellikle lookup tablolarda gerçekleşir. Lookup tablolar kolay kolay ilgili verisinin değişmediği tablolardır.
                var dto = _mapper.Map<AppUserCreateDto>(model);
                var createResponse = await _appUserService.CreateWithRoleAsync(dto,(int)RoleType.Member);
                return this.ResponseRedirectAction(createResponse, "SignIn");
            }
            //kayıt işlemi başarılı değilse ilgili errorları ekleyelim.
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            //tüm gender bilgilerini çekip modelimizin genders kısmına bunları seleclist olarak eklemek tekrar yaparız çünkü hata alındığında kullanıcının seçtiği gender seleclist bilgisi kaybolmasın diye.
            var response = await _genderService.GetAllAsync();
            model.Genders = new SelectList(response.Data, "Id", "Definition",model.GenderId);
            return View(model);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginDto dto)
        {
            var result = await _appUserService.CheckUserAsync(dto);
            if(result.ResponseType == Common.ResponseType.Success)
            {
                //login işlemi gerçekleştirilir.
                //ilgili kullanıcının rollerini çekelim.
                var roleResult = await _appUserService.GetRolesByUserIdAsync(result.Data.Id);
                //role çekme işleminini bir kontrole alalım başarılı bir şekilde gerçekleştimi yani rolü var mı diye.
                var claims = new List<Claim>();
                if(roleResult.ResponseType == Common.ResponseType.Success)
                {
                    foreach (var role in roleResult.Data)
                    {
                        claims.Add(new Claim(ClaimTypes.Role,role.Definition));
                    }
                }
                //rolü olsada olmasada ben bu id yi almak istiyorsam
                claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()));

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = dto.RememberMe,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index","Home");
            }
            //login işlemi gerçekleşmez
            ModelState.AddModelError("Kulanıcı adı veya şifre hatalı", result.Message);
            return View(dto);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
