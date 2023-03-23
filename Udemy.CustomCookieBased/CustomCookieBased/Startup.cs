using CustomCookieBased.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomCookieBased
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //context
            services.AddDbContext<CookieContext>(opt =>
            {
                opt.UseSqlServer("server=DESKTOP-4UF23CE; database=CustomCookieDb; integrated security=true;");
            });

            //CustomCookie i�in Authentication eklemek.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt=>
            {
                opt.Cookie.Name = "CustomCookie";
                opt.Cookie.HttpOnly = true;   //js ile cookie bilgisi �ekmeyi engellemek
                opt.Cookie.SameSite = SameSiteMode.Strict; //cokkieyi payla��ma kapatmak
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; //nas�l geldiyse o�ekilde olu�sun http,https
                opt.ExpireTimeSpan = TimeSpan.FromDays(10); //10 g�n boyunca cookieyi sakla.
                opt.LoginPath = new PathString("/Home/SignIn");
                opt.LogoutPath = new PathString("/Home/LogOut");
                opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
            });

            //mvc
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //�dentity Autholar� kullanabilmek i�in.
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //default route
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
