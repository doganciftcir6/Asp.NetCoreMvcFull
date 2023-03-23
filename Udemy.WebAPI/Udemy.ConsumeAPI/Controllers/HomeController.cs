using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Udemy.ConsumeAPI.ResponseModels;

namespace Udemy.ConsumeAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            //CreateClient metotunu kullnarak ilgil işlemimizi gerçekleştirebiliriz.
            var client = _httpClientFactory.CreateClient();
            //Buraya api uygulamasında launchSettings dosyası içindeki  "applicationUrl": "http://localhost:37540" kısmını veriyoruz.
            //api uygulamasında bulunan productlara ulaşalım GetAsync() metotu isteği yapacağım yeri istiyor. Ve Api uygulamamdaki getall metotu endpointi ile ben bu listelemeyi yapacağım.
            var responseMessage = await client.GetAsync("http://localhost:37540/api/products");
            if(responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //ok olduysa ilgili json tipinde data bana api uygulamasından gelmiştir.
                //json tipinde ilgili datayı okuyalım
                //NewtonSoft kullanarak bunları direkt bir objeye bind edip ele alabilriz.
                //bunun için NewtonSoft paketini indiriyorum direkt Microsoft olan değil.
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                //arından bu json bilgiyi bir objeye dönüştürelim DeserializeObject metotu sayesinde.
                var result = JsonConvert.DeserializeObject<List<ProductResponseModel>>(jsonData);
                //daha sonra bu objeyi artık web projemde kullanabiliyorum view'ıma gönderip dataları kullanabilriim.
                return View(result);
            }
            else
            {
                return View(null);
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductResponseModel model)
        {
            //bir create işlemi yapacağımızı belirtiyoruz
            var client = _httpClientFactory.CreateClient();
            //modelden gelen obje bilgisini Json bilgiye çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(model);
            //bu bilginin httpcontent bilgisine dömesi gerekiyor.
            StringContent content = new StringContent(jsonData,Encoding.UTF8,"application/json");
            //apideki post metotu ile ilgili httpcontent bilgiyi apiye gönderiyoruz kayıt işlemi gerçekleşiyor. 
            var responseMessage = await client.PostAsync("http://localhost:37540/api/products",content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["errorMessage"] = $"Bir hata ile karşılaşıldı. Hata kodu {(int)responseMessage.StatusCode}";
                return View(model);
            }
        }
        //ilgili ürünün bilgilerini getiren metot
        public async Task<IActionResult> Update(int id)
        {
            var client = _httpClientFactory.CreateClient();
            //bir ürünü çeklmeliyim güncelleyeceğim ürünü göstermek için çekeceğim ürünü burda id ile göndermem lazım.
            var responseMessage = await client.GetAsync($"http://localhost:37540/api/products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData =  await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ProductResponseModel>(jsonData);
                return View(data);
            }
            else
            {
                return View(null);
            }
        }
        //bilgileri gelen ürünün update işleminin yapılması
        [HttpPost]
        public async Task<IActionResult> Update(ProductResponseModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            //elimde json tipinde bir string var
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:37540/api/products", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            { 
                return View(model);
            }
        }
        public async Task<IActionResult> Remove(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"http://localhost:37540/api/products/{id}");
            return RedirectToAction("Index");
        }
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var client = _httpClientFactory.CreateClient();

            //doyayı byte'a çevirmek
            //System.IO.File.ReadAllBytes(file.FileName);
            //güvenli olanı
            //stream'a ilgili dosyayı kopyalamak
            var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            //bytle'ları okumak
            var bytes = stream.ToArray();

            //Byte dizisi 
            ByteArrayContent content = new ByteArrayContent(bytes);
            //contentType'ını kaybetmemek için bu contentType ile atıyorum sadece png dosyaları eklensin yapabiliyorum. Belirli kısıtlamara tabi tutabilirim. ContentType'ı şu şu olan şeyleri bana gönder bunlardan  biri değilse badrequest.
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(content,"formFile",file.FileName);

            await client.PostAsync("http://localhost:37540/api/products/upload", formData);
            return RedirectToAction("Index");
        }
    }
}
