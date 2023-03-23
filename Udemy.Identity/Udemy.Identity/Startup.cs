using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Udemy.Identity.Context;
using Udemy.Identity.CustomDescriber;
using Udemy.Identity.Entities;

namespace Udemy.Identity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //�dentity eklemek
            //varsay�lan olarak gelen validationlar� ayarlamak
            services.AddIdentity<AppUser,AppRole>(opt=> {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                //kilitlenme s�resini de�i�tirme.
                //opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                //fail deneme say�s�sn� de�i�tirmek 3 ten fazla olursa kilitlensin.
                opt.Lockout.MaxFailedAccessAttempts = 3;
                //CUSTOM ERROR MESAJLARINI KULLANMAK
            }).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<UdemyContext>();

            //cookie ayarlar�n� de�i�tirmek
            services.ConfigureApplicationCookie(opt =>
            {
                //true yazd���m�zda di�er �ah�slar taraf�ndan javascript arac�l���yla �ekilemiyor cookie.
                opt.Cookie.HttpOnly = true;
                //Sadece ilgili domainde kullanabilir
                opt.Cookie.SameSite = SameSiteMode.Strict;
                //always dersek sadece https'de �al���r SameAsRequest dersek http ile gelirse httpde, https ile gelirse https'de �al���r
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                //Cookie'nin name'ini de�i�tirmek
                opt.Cookie.Name = "UdemyCookie";
                //cookkie'nin ayakta kalma s�resini ayarlamak
                opt.ExpireTimeSpan = TimeSpan.FromDays(25);
                //bir kullan�c�n�n yetkisi olmadan bir alana giri� yapmaya �al��t���nda onu nereye g�nderece�imizi ayarlayal�m
                opt.LoginPath = new PathString("/Home/SignIn");
                //AccessDeniedPath path'ini �zelle�tirmek yani giri� yap�ld�ktan sonra ilgili kullan�c� yetkisiz bir yere girmeye �al���rsa buraya y�nlendirilsin.
                opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
            });

            //sql server ba�lant�s�
            services.AddDbContext<UdemyContext>(opt =>
            {
                opt.UseSqlServer("server = DESKTOP-4UF23CE; database = IdentityDb; integrated security = true; ");
            });
            //mvc kurulumu
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //wwwroot
            app.UseStaticFiles();

            //node module
            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath="/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules"))
            });

            app.UseRouting();

            //yetkilendirme
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //default bir router
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
