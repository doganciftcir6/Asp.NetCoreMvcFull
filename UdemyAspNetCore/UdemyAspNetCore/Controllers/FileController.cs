using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace UdemyAspNetCore.Controllers
{
    public class FileController:Controller
    {
        public IActionResult List()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files"));
            //bu path'teki dosyaları görmek istiyorum
            var files = directoryInfo.GetFiles();
            return View(files);
        }
        public IActionResult Create()
        {
            return View();
        }
        //ilgili inputun name="fileName" inden gelen kullancının girdiği bilgiyi model binding ile yakalıyoruz (string fileName) sayesinde
        [HttpPost]
        public IActionResult Create(string fileName)
        {
            //boş bir dosya oluşturmak
            FileInfo fileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileName));
            //kontrol ediyoruz aynı isimde dosya var mı diye
            if (!fileInfo.Exists) 
            {
                //demek ki dosya yok create yapalım
                fileInfo.Create();
            }
            return RedirectToAction("List");
        }
        public IActionResult Remove(string fileName)
        {
            //dosya bilgisini almak fileName bilgisi kullanıcının tıkladığı a etiketinden asp-route-fileName="@item.Name" dan geliyor.
            FileInfo fileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileName));
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            return RedirectToAction("List");
        }
        //otomatik bir txt dosyası oluşturmak file/createWithData dendiği zaman urlye.
        public IActionResult CreateWithData()
        {
            //boş bir dosya oluşturmak
            FileInfo fileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", Guid.NewGuid().ToString()+".txt"));
            StreamWriter writer = fileInfo.CreateText();
            writer.Write("Merhaba ben yavuz");
            writer.Close();
            return RedirectToAction("List");
        }
        //upload yapmak
        public IActionResult Upload()
        {
            return View();
        }
        //(IFormFile formFile) ilgili inputun name alanına formFile yazmıştık aynı olmasına dikkat databinding için kullanıcın yüklediği dosyanın bilgilerine bu sayede ulaşıyorum.
        [HttpPost]
        public IActionResult Upload(IFormFile formFile)
        {
            //sadece png dosyasının yüklenmesine izin verebiliriz
            if (formFile.ContentType == "image/png") {
            //dosyanın uzantısını alalım
            var ext = Path.GetExtension(formFile.FileName);

            //dosyanın yolu lazım path
            //ilgili dosyanın adına yani kullanıcının yüklediği dosyayanın adını benzersiz bir isim yapıyorum ve uzantısını ekliyorum.
           var path =  Directory.GetCurrentDirectory() + "/wwwroot" + "/images/" + Guid.NewGuid() + ext;

            FileStream stream = new FileStream(path,FileMode.Create);
            formFile.CopyTo(stream);
            TempData["message"] = "Dosya upload işlemi başarıyla gerçekleşti.";


            }
            else
            {
                TempData["message"] = "Dosya upload edilemedi uygunsuz dosya tipi.";
            }
            return RedirectToAction("Upload");
        }
    }
}
