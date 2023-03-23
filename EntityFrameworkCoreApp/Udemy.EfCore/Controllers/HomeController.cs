using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Udemy.EfCore.Data.Contexts;
using Udemy.EfCore.Data.Entities;

namespace Udemy.EfCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //context'i kullanabilmek için
            UdemyContext context = new UdemyContext();

            //PRODUCT TABLOSUNA VERİ EKLEMEK ADDİNG ***********************************************
            //context.Products.Add(new Product {
            //    Name = "Telefon",
            //    Price = 3400
            //});
            //eklemeye yapacağımı belirttim state durumuna added düştü şimdi ekleme işlemini yapıcam
            //context.SaveChanges();

            //VERİYİ GÜNCELLEMEK UPDATE***********************************************
            //var updatedProduct = context.Products.Find(1);
            //updatedProduct.Price = 4000;
            //context.Products.Update(updatedProduct);
            //context.SaveChanges();

            //VERİYİ TABLODAN SİLMEK **********************************************************
            //var deletedProduct = context.Products.FirstOrDefault(x => x.Id == 1);
            //context.Products.Remove(deletedProduct);
            //context.SaveChanges();

            //ürün oluşturma ve databaseye ekleme
            //Product product = new Product()
            //{
            //    Price = 4000
            //};
            //context.Products.Add(product);
            //context.SaveChanges();

            //return View();

            //kalıtım olduğu için bunu yapabiliyorum.
            //TABLE PER HİERARCHY 
            context.Employees.Add(new PartTimeEmployee 
            {
                DailyMage = 400,
                FirstName = "part",
                LastName="part"
            });
            context.Employees.Add(new PartTimeEmployee
            {
                DailyMage = 400,
                FirstName = "part2",
                LastName = "part2"
            });
            context.Employees.Add(new FullTimeEmployee
            {
                HourlyMage = 60,
                FirstName = "full",
                LastName = "full"
            });
            context.SaveChanges();
            //BÜTÜN VERİLERİ ÇEKMEK İSTESEM
            var parts = context.PartTimeEmployees.ToList();
            var parts2 = context.PartTimeEmployees.Where(x => x is PartTimeEmployee).ToList();
            return View();
        }
    }
}
