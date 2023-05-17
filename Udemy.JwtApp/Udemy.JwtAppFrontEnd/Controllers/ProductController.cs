using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Udemy.JwtAppFrontEnd.Models;

namespace Udemy.JwtAppFrontEnd.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> List()
        {
            //token bilgime nasıl erişicem
            var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                //clienti ele alıcam
                var client = _httpClientFactory.CreateClient();
                //token'ı nasıl göndericem
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                //artık isteiğimi yapabilirim
                var response = await client.GetAsync("http://localhost:5098/api/Products");
                if (response.IsSuccessStatusCode)
                {
                    //başarılıysa isteğim response'ın contentini okucam
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<ProductListModel>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });
                    return View(list);
                }
            }
            return View();
        }
        public async Task<IActionResult> Remove(int id)
        {
            //token bilgime nasıl erişicem
            var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                //clienti ele alıcam
                var client = _httpClientFactory.CreateClient();
                //alınan tokenı ekleyelim
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                //artık isteiğimi yapabilirim
                await client.DeleteAsync($"http://localhost:5098/api/Products/{id}");
            }
            return RedirectToAction("List");
        }
        public async Task<IActionResult> Create()
        {
            var model = new ProductCreateModel();
            //selectlisti doldurmak için backende gidip categorileri istekleyip çekmem lazım.
            //token bilgime nasıl erişicem
            var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                //clienti ele alıcam
                var client = _httpClientFactory.CreateClient();
                //alınan tokenı ekleyelim
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                //artık isteiğimi yapabilirim
                var response = await client.GetAsync("http://localhost:5098/api/Categories");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<List<CategoryListResponseModel>>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });
                    model.Categories = new SelectList(data, "Id", "Definition");
                    return View(model);
                }
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            //kayıt işlemi başarısız olursa validationa takılırsa seleclist verileri kaybolmasın (bu yöntem sayesinde backende tekrar gitmek zorunda kalmıyoruz yani bir kere çektiğimiz veriyi içeride pasladık tekrar çekmeye uğraşmadık)
            var data = TempData["Categories"]?.ToString();
            if (data != null)
            {
                var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);
                model.Categories = new SelectList(categories, "Value", "Text");
            }

            if (ModelState.IsValid)
            {
                //selectlisti doldurmak için backende gidip categorileri istekleyip çekmem lazım.
                //token bilgime nasıl erişicem
                var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    //clienti ele alıcam
                    var client = _httpClientFactory.CreateClient();
                    //alınan tokenı ekleyelim
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    //modeli jsondataya çevirelim
                    var jsonData = JsonSerializer.Serialize(model);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    //artık isteiğimi yapabilirim
                    var response = await client.PostAsync("http://localhost:5098/api/Products", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List");
                    }
                    ModelState.AddModelError("", "Bir hata oluştu!");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            //token işlerinden sonra önce id sine göre producti çekiyoruz sonra işlem başarılıysa categoryleri çekiyoruz selecliste ekliyoruz.
            //token bilgime nasıl erişicem
            var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                //clienti ele alıcam
                var client = _httpClientFactory.CreateClient();
                //token'ı nasıl göndericem
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var responseProduct = await client.GetAsync($"http://localhost:5098/api/Products/{id}");
                if (responseProduct.IsSuccessStatusCode)
                {
                    //başarılıysa isteğim response'ın contentini okucam
                    var jsonString = await responseProduct.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<UpdateProductModel>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });
                    //category çekme alanı selectlist için
                    var responseCategory = await client.GetAsync("http://localhost:5098/api/Categories");
                    if (responseCategory.IsSuccessStatusCode)
                    {
                        var jsonCategoryData = await responseCategory.Content.ReadAsStringAsync();
                        var data = JsonSerializer.Deserialize<List<CategoryListResponseModel>>(jsonCategoryData, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        });
                        if (result != null)
                        {
                            result.Categories = new SelectList(data, "Id", "Definition");
                        }
                    }
                    return View(result);
                }
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductModel model)
        {
            //kayıt işlemi başarısız olursa validationa takılırsa seleclist verileri kaybolmasın (bu yöntem sayesinde backende tekrar gitmek zorunda kalmıyoruz yani bir kere çektiğimiz veriyi içeride pasladık tekrar çekmeye uğraşmadık)
            var data = TempData["Categories"]?.ToString();
            if (data != null)
            {
                var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);
                //burda farklı olarak model.Categoryıd veriyorum
                model.Categories = new SelectList(categories, "Value", "Text", model.CategoryId);
            }

            if (ModelState.IsValid)
            {
                //selectlisti doldurmak için backende gidip categorileri istekleyip çekmem lazım.
                //token bilgime nasıl erişicem
                var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    //clienti ele alıcam
                    var client = _httpClientFactory.CreateClient();
                    //alınan tokenı ekleyelim
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    //modeli jsondataya çevirelim
                    var jsonData = JsonSerializer.Serialize(model);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    //artık isteiğimi yapabilirim
                    var response = await client.PutAsync("http://localhost:5098/api/Products", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List");
                    }
                    ModelState.AddModelError("", "Bir hata oluştu!");
                }
            }
            return View(model);
        }
    }
}
