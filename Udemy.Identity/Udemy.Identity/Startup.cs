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
            //ýdentity eklemek
            //varsayýlan olarak gelen validationlarý ayarlamak
            services.AddIdentity<AppUser,AppRole>(opt=> {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                //kilitlenme süresini deðiþtirme.
                //opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                //fail deneme sayýsýsný deðiþtirmek 3 ten fazla olursa kilitlensin.
                opt.Lockout.MaxFailedAccessAttempts = 3;
                //CUSTOM ERROR MESAJLARINI KULLANMAK
            }).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<UdemyContext>();

            //cookie ayarlarýný deðiþtirmek
            services.ConfigureApplicationCookie(opt =>
            {
                //true yazdýðýmýzda diðer þahýslar tarafýndan javascript aracýlýðýyla çekilemiyor cookie.
                opt.Cookie.HttpOnly = true;
                //Sadece ilgili domainde kullanabilir
                opt.Cookie.SameSite = SameSiteMode.Strict;
                //always dersek sadece https'de çalýþýr SameAsRequest dersek http ile gelirse httpde, https ile gelirse https'de çalýþýr
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                //Cookie'nin name'ini deðiþtirmek
                opt.Cookie.Name = "UdemyCookie";
                //cookkie'nin ayakta kalma süresini ayarlamak
                opt.ExpireTimeSpan = TimeSpan.FromDays(25);
                //bir kullanýcýnýn yetkisi olmadan bir alana giriþ yapmaya çalýþtýðýnda onu nereye göndereceðimizi ayarlayalým
                opt.LoginPath = new PathString("/Home/SignIn");
                //AccessDeniedPath path'ini özelleþtirmek yani giriþ yapýldýktan sonra ilgili kullanýcý yetkisiz bir yere girmeye çalýþýrsa buraya yönlendirilsin.
                opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
            });

            //sql server baðlantýsý
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
