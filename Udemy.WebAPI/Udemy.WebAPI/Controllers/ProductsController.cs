using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Udemy.WebAPI.Data;
using Udemy.WebAPI.Interfaces;

namespace Udemy.WebAPI.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //api/products ~ GET
        //api/products/id ~ GET/DELETE
        //api/products ~ POST/PUT

        //geriye bunları dönmeliyiz
        //Ok(200), NotFound(404), NoContent(204), Created(201), BadRequest(400)

        //PRODUCT TABLOSUNUN KAYITLARINI LİSTELEMEK. (RESTE UYGUN ŞEKİLDE)
        //Bu kişi benden bir token almalı'ki bu listeleme işini yapabilsin.
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productRepository.GetAllAsync();
            return Ok(result);
        }

        //PRODUCT TABLOSUNUN ID YE GÖRE KAYITLARINI LİSTELEMEK. (RESTE UYGUN ŞEKİLDE)
        //api/products/1
        //burda ("{id}") verildiğinde varsayılan olarak aslında [FromRoute] oluyor. İd değerini Route'dan bekliyorum anlamına geliyor.
        //eğer api/products?id=1 yapsaydık bu varsayılan olarak [FromQuery] oluyor. Yani [httpget("getById")] yazarsak. Yani id değerini Query'den bekleyecektir.
        //bu endpoint'e sadece Memberlar ve giriş yapmuş olanlar erişebilecek.
        [Authorize(Roles = "Member")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var data =  await _productRepository.GetByIdAsync(id);
            if(data == null)
            {
                return NotFound(id);
            }
            else
            {
                return Ok(data);
            }
        }
        //PRODUCT TABLOSUNA KAYIT EKLEMEK. (RESTE UYGUN ŞEKİLDE)
        //metot HttpPost ise ve parametre alıyorsa aslında burada varsayılan olarak [FromBody] vardır.
        //[FromQuery] dersek api/products?id=1&name=telefon/ bu şekilde devam etmesi gerekecek.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Product product)
        {
            var addedProduct = await _productRepository.CreateAsync(product);
            return Created(string.Empty,addedProduct);
        }
        //PRODUCT TABLOSUNA KAYIT GÜNCELLEMEK. (RESTE UYGUN ŞEKİLDE)
        //Burada .netcore otomatik olarak bu metota [FormBody] der.
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            //bana gelen ıd'de bir product var mı
            var checkProduct = await _productRepository.GetByIdAsync(product.Id);
            if(checkProduct == null)
            {
                //ilgili id'li kayıt db'de yoktur.
                return NotFound(product.Id); 
            }
            else
            {
                //ilgili id'li kayıt db'de mevcut. Update işlemi yapılabilir.
                await _productRepository.UpdateAsync(product);
                return NoContent();
            }
        }
        //PRODUCT TABLOSUNA KAYIT SİLMEK. (RESTE UYGUN ŞEKİLDE)
        //.NetCore bu metota otomatik olarak [FromRoute] der.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //ilgili id'li kayıt db'De var mı diye check edelim.
            var checkProduct = _productRepository.GetByIdAsync(id);
            if(checkProduct == null)
            {
                //ilgili id'li kayıt db'de yoktur.
                return NotFound(id);
            }
            else
            {
                //ilgili id'li kayıt db'De mevcut silme işlemine uygun.
                await _productRepository.DeleteAsync(id);
                return NoContent();
            }
        }
        //PRODUCT TABLOSUNA UPLOAD.(RESTE UYGUN ŞEKİLDE)
        //api/product/upload
        //[FromForm] diyerek buradaki formFile bilgisi Formdan metota gelecek diye belirtmeliyiz.
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile formFile)
        {
            var newName = Guid.NewGuid() + "." + Path.GetExtension(formFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", newName);
            var stream = new FileStream(path, FileMode.Create);
            await formFile.CopyToAsync(stream);
            return Created(string.Empty, formFile);
        }


        //ben belli bir dependency injection'u sadecebu metotta, bir yerde kullanmak istiyorum bu durumda [FromServices] kullanabilirim. 
        //ben [FromForm] ile formdan gelen name datasını çekmek istersem kullanabilirim
        //[FromHeader] ile rquestin header veya body alanındaki alanlara erişmek istediğimde kullanabilirim.
        [HttpGet("[action]")]
        //[FromForm] string name, [FromHeader] string auth
        public IActionResult Test([FromServices] IDummyRepository dummyRepository)
        {
            //request       =>  response
            //header, body

            //var authentication = HttpContext.Request.Headers["auth"];

            //var name2 = HttpContext.Request.Form["name"];
            //return Ok();

            return Ok(dummyRepository.GetName());
        }
    }
}
