using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using UdemyAspNetCore.Filters;
using UdemyAspNetCore.Models;

namespace UdemyAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        //ysk.com/Home/Index
        public IActionResult Index()
        {
            //•	Bir interface örneklenemez ancak bir interface bir nesne örneği taşıyabilir.Kendisinden kalıtılan nesne örneğini taşıyabilir.
            //ITest test = new Test();
            //ITest test2 = new Test2();

            ////Route'dan data çekmek
            //var values = (string)RouteData.Values["id"];

            //ViewBag.Name = "Yavuz";
            //ViewData["Name"] = "Selim";

            //Customer customer = new Customer() { Age = 27, FirstName = "Yavuz Selim", LastName = "Kahraman" };
            //return View(customer);

            //veritabanındaki tüm customer'ları alalım ve cshtml sayfaya bir model olarak gönderelim
            var customers = CustomerContext.Customers;
            return View(customers);
        }
        public IActionResult Yavuz()
        {
            //businnes => db veri çekiyor
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidFirstName]
        //Model Binding
        //CSHTML dosyasındaki inputlardan asp-for sayesinde kullanıcının inputa girdiği VALUE bilgisi buraya ulaşıyor o bilgiyi Customer customer sayesinde alabiliyoruz. Aslında tüm bilgiler ulaşıyor ama asp-for bu bilgileri otoamtik dolduruyor TYPE ve NAME bilgisini otomatik dolduruyor bizi uğraştırmıyor bizde sadece bu şekilde bu bilgileri veritbanına ekliyoruz.
        //model binding ile formdan gelen bilgileri eşzamanlı olarak yakalayabilriiz
        public IActionResult Create(Customer customer)
        {
            //inputların name özelliği bilgileri sayesinde içlerine yazılan bilgilere erişip onları bir değişkene atıyoruz
            //var firstName = HttpContext.Request.Form["firstName"].ToString();
            //var lastName = HttpContext.Request.Form["lastName"].ToString();
            //var age = int.Parse(HttpContext.Request.Form["age"].ToString());
            //input içindeki verilere eriştik şimdi ıd'yi otomatik artar şekilde ayarlayıp ilgili context'e yeni verileri ekleyelim.

            //validation kuralları
            ModelState.Remove("Id");
            if(customer.FirstName == "Yavuz")
            {
                ModelState.AddModelError("", "Firstname yavuz olamaz");
            }
            if (ModelState.IsValid)
            {
                //ekleme işlemini gerçekleştir
                Customer lastCustomer = null;
                if (CustomerContext.Customers.Count > 0)
                {
                    lastCustomer = CustomerContext.Customers.Last();
                }
                customer.Id = 1;
                if (lastCustomer != null)
                {
                    customer.Id = lastCustomer.Id + 1;
                }
                //ıd'yi otomatik artar şekilde yaptık şimdi yeni değişkenlerimizi yani input içine girilen yeni bilgilerimizi veritabanındaki ilgili kolonların içerisine ekleyelim
                /*CustomerContext.Customers.Add(new Customer { Age = age, FirstName = firstName, LastName = lastName }); */

                //model binding'ten sonra
                //asp-for'un doldurduğu bilgileri veritabanına ekliyoruz
                CustomerContext.Customers.Add(customer);
                //işlem bittikten sonra kullanıcıyı ürün listesi sayfasına yönlendirelim
                return RedirectToAction("Index");
            }
            //validation uymadıysa
            return View();

        
        }
        //Remove için bir id bekliyoruz id'yi model binding ile arlabiliriz.
        public IActionResult Remove(int id)
        {
            //routtan gelen id'yi yakalamak
            //var id = int.Parse(RouteData.Values["id"].ToString());
            //url'deki id ile veritabanındaki ıd si aynı olan ürünü buluyoruz
           var removedCustomer = CustomerContext.Customers.Find(a => a.Id == id);
            //bulunan ürünü veritabanından siliyoruz
            CustomerContext.Customers.Remove(removedCustomer);
            //işlem bitince kullanıcıyı ürün listesi sayfasına gönderiyoruz
            return RedirectToAction("Index");
        }
        //route datadan gelen id bilgisini model binding ile almak
        [HttpGet]
        public IActionResult Update(int id)
        {
            //bu işlemler seçilen ürünün hali hazırda olan bilgilerini input içinde dolu olarak gelmesi için yapılır.
            //url'ye gelen id yi yakalamak
            //var id = int.Parse(RouteData.Values["id"].ToString());
            //Where metotu func delegate ister.
            //url ye gelen ıd ile veritabanında olan id'nin eşitlenip ilgili ürünün bulunmasını sağlıyoruz.
            var updatedCustomer=  CustomerContext.Customers.FirstOrDefault(a=> a.Id == id);
            //model aracılığıyla ilgili personeli Vİew'e gönderiyorum
            return View(updatedCustomer);
        }
        //model binding ile formdan gelen bilgileri eşzamanlı olarak yakalayabilriiz
        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            //güncelleyeğimiz customer'ı yakalamak
            //var id = int.Parse(HttpContext.Request.Form["id"].ToString());
            //ilgili id değerini FirstOrDefault içerisine func delegate ile geçicem
            //diyeceğim ki Id'si forumdaki ıd'ye eşit olan customer'ı bana bul
            //yani kullanıcı ürün seçip ona update basmış daha sonra o ürünün hidden inputunun valuesine ürünün id bilgisi düşmüş biz o value'deki id bilgisiyle veritabanındaki id'si o sayıya eşit olan ürünü seç diyoruz.
            var updatedCustomer = CustomerContext.Customers.FirstOrDefault(i=> i.Id == customer.Id);
            //idler eşleştikten sonra bulunan customer'ın alanlarını güncellicem bu güncelleme inputların name özellikleri sayesinde oluyor
            //updatedCustomer.FirstName = HttpContext.Request.Form["firstName"].ToString();
            //updatedCustomer.LastName = HttpContext.Request.Form["lastName"].ToString();
            //updatedCustomer.Age = int.Parse(HttpContext.Request.Form["age"].ToString());
            //işlem bittikten sonra kullanıcıyı ürün listesi sayfasına yönlendirelim
            updatedCustomer.FirstName = customer.FirstName;
            updatedCustomer.LastName = customer.LastName;
            updatedCustomer.Age = customer.Age;
            return RedirectToAction("Index");
        }
        //custom status code page
        public IActionResult Status(int? code)
        {
            return View();
        }
        //global excepiton hangling uygulama içi hataları
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //LOGLAMA
            //serilog nlog
            //11-02-2020_saat

            //Klasör yolunun path bilgsini alalım
            var logFolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","logs");
            //Dosya yolunun path bilgisini alalım
            //11/02/2020 15:30:12 normalde böyle bir bilgi gelir
            var logFileName = DateTime.Now.ToString();
            logFileName = logFileName.Replace(" ", "_");
            logFileName = logFileName.Replace(":", "-");
            logFileName = logFileName.Replace("/", "-");
            logFileName += ".txt";
            var logFilePath = Path.Combine(logFolderPath, logFileName);
            //
            DirectoryInfo directoryInfo = new DirectoryInfo(logFolderPath);
            //eğer bu dosya ismiyle dosya yoksa bu dosyayı oluşturalım
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            //klasörü oluşturduk şimdi klasör içine geçip dosyayı oluşturalım
            FileInfo fileInfo = new FileInfo(logFilePath);
            //txt dosyasını oluşturalım
            var writer = fileInfo.CreateText();
            writer.WriteLine("Hatanın gerçekleştiği yer: " + exceptionHandlerPathFeature.Path);
            writer.WriteLine("Hata mesajı: " + exceptionHandlerPathFeature.Error.Message);
            writer.Close();
            return View();
        }
        //hata oluşturmak için
        public IActionResult Hata()
        {
            throw new System.Exception("Sistemsel hata oluştu");
        }

        public interface ITest
        {

        }
        public class Test : ITest
        {

        }
        public class Test2 : ITest
        {

        }
    }
}
