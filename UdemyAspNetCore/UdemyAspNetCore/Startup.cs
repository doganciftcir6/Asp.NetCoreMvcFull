using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UdemyAspNetCore.Middlewares;

namespace UdemyAspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Appsetting.json datasýný çekmek
            var firstName = configuration.GetSection("Person:FirstName").Value;
            var lastName = configuration.GetSection("Person:Lastname").Value;

            //global excepiton hangling uygulama içi hatalarý
            app.UseExceptionHandler("/Home/Error");
            //custom status code page
            app.UseStatusCodePagesWithReExecute("/Home/Status", "?code={0}");

            app.UseHttpsRedirection();
            //wwwroot dosyasýný dýþarýya açmak
            app.UseStaticFiles();
            //node_module dosyasýný dýþarýya açmak kütüphaneleri kullanabilmek için bootstrap vs.
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"node_modules"))
            });

           
            //url anlamlý hale gelmesi için
            app.UseRouting();
            //session kullanabilmek için
            app.UseSession();

            //middleware kullanabilmek için
            //app.UseMiddleware<ResponseEditingMiddleware>();
            //app.UseMiddleware<RequestEditingMiddleware>();

            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                //Arealar için bir route
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{Area}/{Controller=Home}/{Action=Index}/{id?}"
                    );

                //endpoints.MapControllerRoute(
                //    name: "productRoute",
                //    pattern: "Yavuz/{Action}",
                //    defaults: new { Controller = "Home"}
                //    );
               //localhost/Home(Controller)/Index(Action)/1(id)(?opsiyonel)
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{Controller}/{Action}/{id?}",
                    defaults: new {Controller="Home",Action="Index"}
                    );
            });
        }
    }
}
