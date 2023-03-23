using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.IO;

namespace UdemyAspNetCore.Controllers
{
    public class FolderController : Controller
    {
        public IActionResult List()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot"));
            //istiyorum ki bu klasörler gelsin bana
            var folders = directoryInfo.GetDirectories();
            return View(folders);
        }
        public IActionResult Create()
        {

            return View();  
        }
        //databinding'teki folderName bize input'un name özelliğinden gelecek
        [HttpPost]
        public IActionResult Create(string folderName)
        {
            //dosya oluştururken uygulamanın çalıştığı konumun içinde wwwroot klasörünün içinde kullanıcının inputa girdiği name özelliğine gelen değeeri model binding ile yakalayıp klasör isminide buraya ekliyoruz sistem/wwwroot/name şeklinde
            DirectoryInfo info = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",folderName));
            //kontrol info yani dosya oluşturulmamışsa aynı isimli dosya yoksa oluşturma yapılsın
            if (!info.Exists)
            {
                info.Create();
            }
            return RedirectToAction("List");
        }
        public IActionResult Remove(string folderName)
        {
            //burada ise bir formumuz yok dolasıyıla silme butonuna asp-route-folderName="@item.Name" verirsek bu buradaki model-binding ile eş zamanlı çalışır yani kullanıcı hangi kayıdın silme butonuna tıkalrsa bu @item.name alanına o kayıdın name bilgisi düşer ve bu bilgi de burada server tarafında string folderName ile yakalanır.
            DirectoryInfo info = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName));
            //eğer bilgi gerçekten varsa silme işlemini yaparız
            if (info.Exists)
            {
                //Delete metotuna true demezsek eğer silenecek dosyanın içeriğinde bir şey varsa başka bir dosya yani silme işlemi gerçekleşmez
                info.Delete(true);
            }
            return RedirectToAction("List");
        }
    }
}
