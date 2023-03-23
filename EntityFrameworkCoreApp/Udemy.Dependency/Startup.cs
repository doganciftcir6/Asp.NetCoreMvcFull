using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.Dependency.Controllers;
using Udemy.Dependency.Services;

namespace Udemy.Dependency
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IScopedService, ScopedManager>();
            services.AddSingleton<ISingletonService, SingletonManager>();
            services.AddTransient<ITransientService, TransientManager>();
            //dependenct injection
            services.AddScoped<IProductService,ProductManager>();
            //mvc eklemek
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

            app.UseEndpoints(endpoints =>
            {
                //Controller = Home Action = Index Id?
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
