using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using Udemy.JwtAppFrontEnd.Models;

namespace Udemy.JwtAppFrontEnd.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> List()
        {
            //clienti ele alıcam bunun için en altta oluşturduğum metotu kullanıcam.
            var client = this.CreateClient();

            //artık isteiğimi yapabilirim
            var response = await client.GetAsync("http://localhost:5098/api/Categories");
            if (response.IsSuccessStatusCode)
            {
                //başarılıysa isteğim response'ın contentini okucam
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<CategoryListResponseModel>>(jsonString,new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
                return View(list);
            }
            //else if(response.StatusCode == HttpStatusCode.Forbidden)
            //{
            //    return RedirectToAction("AccessDenied", "Account");
            //}
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        public async Task<IActionResult> Remove(int id)
        {
            //clienti ele alıcam bunun için en altta oluşturduğum metotu kullanıcam.
            var client = this.CreateClient();

            //artık isteiğimi yapabilirim
            await client.DeleteAsync($"http://localhost:5098/api/Categories/{id}");
            return RedirectToAction("List");

        }

        public IActionResult Create()
        {
            return View(new CategoryCreateRequestModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }












































        //tekrar tekrar yaptığım işlemleri bu metot içine alıcam.
        private HttpClient CreateClient()
        {
            //clienti ele alıcam
            var client = _httpClientFactory.CreateClient();
            //token bilgime nasıl erişicem
            var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken")?.Value;
            //token'ı nasıl göndericem
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}
