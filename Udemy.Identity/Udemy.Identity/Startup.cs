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
            //ıdentity eklemek
            //varsayılan olarak gelen validationları ayarlamak
            services.AddIdentity<AppUser,AppRole>(opt=> {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                //kilitlenme süresini değiştirme.
                //opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                //fail deneme sayısısnı değiştirmek 3 ten fazla olursa kilitlensin.
                opt.Lockout.MaxFailedAccessAttempts = 3;
                //CUSTOM ERROR MESAJLARINI KULLANMAK
            }).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<UdemyContext>();

            //cookie ayarlarını değiştirmek
            services.ConfigureApplicationCookie(opt =>
            {
                //true yazdığımızda diğer şahıslar tarafından javascript aracılığıyla çekilemiyor cookie.
                opt.Cookie.HttpOnly = true;
                //Sadece ilgili domainde kullanabilir
                opt.Cookie.SameSite = SameSiteMode.Strict;
                //always dersek sadece https'de çalışır SameAsRequest dersek http ile gelirse httpde, https ile gelirse https'de çalışır
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                //Cookie'nin name'ini değiştirmek
                opt.Cookie.Name = "UdemyCookie";
                //cookkie'nin ayakta kalma süresini ayarlamak
                opt.ExpireTimeSpan = TimeSpan.FromDays(25);
                //bir kullanıcının yetkisi olmadan bir alana giriş yapmaya çalıştığında onu nereye göndereceğimizi ayarlayalım
                opt.LoginPath = new PathString("/Home/SignIn");
                //AccessDeniedPath path'ini özelleştirmek yani giriş yapıldıktan sonra ilgili kullanıcı yetkisiz bir yere girmeye çalışırsa buraya yönlendirilsin.
                opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
            });

            //sql server bağlantısı
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
