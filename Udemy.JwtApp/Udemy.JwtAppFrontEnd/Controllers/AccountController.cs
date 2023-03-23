using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using Udemy.JwtAppFrontEnd.Models;

namespace Udemy.JwtAppFrontEnd.Controllers
{
    public class AccountController : Controller
    {
        //APİ
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }



        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginModel model)
        {
            //asp.net core içerisinde bulun Authorazion yapısını kullanıcaz.
            //_httpClientFactory ile bir request atıcaz eğer request başarılı ise bana bir token döndüyse(burası apiyle ilgili olan kısım bundan sonrası frontend) ilgili token'ı okuduktan sonra HttpContext.SignInAsync ile signin edicem.

            //önce api tarafındaki http://localhost:5098 ile bir request atıcaz.
            var client = _httpClientFactory.CreateClient();
            //modelimizi Json bilgiye çeviricez.
            var requestContent = new StringContent(JsonSerializer.Serialize(model),Encoding.UTF8,"application/json");
            //bu noktaya istek yapacaksın diyoruz
            var response = await client.PostAsync("http://localhost:5098/api/Auth/SignIn", requestContent);
            //dönen cevap başarılıysa diye bir kontrol.
            if (response.IsSuccessStatusCode)
            {
                //sıkıntı yok demek ki token alabildim.
                //önce contentini okucam. Burda bana gelen data aslında JwtTokenResponse json data yani.
                var jsonData = await response.Content.ReadAsStringAsync();
                //bu datayı benim deserialize etmem lazım.
                //PropertyNamingPolicy = JsonNamingPolicy.CamelCase, yaparak neye çevireceğini söylüyoruz.
                var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
                //Jason datayı normal modelimin data türüne çevirdim şimdi bu sefer write değil read.
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenModel?.Token);
                //artık elimde doğrudan token'ın kendisi var.
                if(token != null)
                {
                    //AŞAĞIDAKİ GİBİ BİR KULLANIM YAPABİLİRİZ KULLANILAN BİR YAPIDIR.
                    ////tokenın içerisindeki claimleri alalım. role bilgilerini alalım.
                    //var roles = token.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
                    //if (roles.Contains("Admin"))
                    //{
                    //    //Redirect..
                    //}


                    var claims = token.Claims.ToList();
                    claims.Add(new Claim("accessToken", tokenModel?.Token == null ? "":tokenModel.Token));

                    ClaimsIdentity identity = new ClaimsIdentity(claims,JwtBearerDefaults.AuthenticationScheme);
                    var authProps = new AuthenticationProperties()
                    {
                        //tokenın süresini sürekli yenilemesin diye
                        AllowRefresh = false,

                        ExpiresUtc = tokenModel?.ExpireDate,
                        IsPersistent = true,
                    };

                   await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProps);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //EĞER TOKEN NULL İSE   
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalıdır.");
                    return View(model);
                }

            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalıdır.");
                return View(model);
            }
        }
    }
}
