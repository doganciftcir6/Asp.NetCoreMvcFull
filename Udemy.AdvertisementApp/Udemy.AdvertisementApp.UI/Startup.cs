using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.AdvertisementApp.Business.DependencyResolvers.Microsoft;
using Udemy.AdvertisementApp.Business.Helpers;
using Udemy.AdvertisementApp.UI.Mappings.AutoMapper;
using Udemy.AdvertisementApp.UI.Models;
using Udemy.AdvertisementApp.UI.ValidationRules;

namespace Udemy.AdvertisementApp.UI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        //sql baðlantýsý için
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //DependencyExtension unu kullanabilmek.
            services.AddDependencies(Configuration);
            //modelin validatörlerini kullabilmek için
            services.AddTransient<IValidator<UserCreateModel>, UserCreateModelValidator>();
            //custom Aythentication için (giriþ iþlemleri) ayrýca Cookie Configuration'larý
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                opt.Cookie.Name = "UdemyCokkie";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);
                opt.LoginPath = new PathString("/Account/SignIn");
                opt.LogoutPath = new PathString("/Account/LogOut");
                opt.AccessDeniedPath = new PathString("/Account/AccessDenied");
            });
            //mvc
            services.AddControllersWithViews();

            //modelin automapper'ý için helper'da oluþturduðum classý ve metotu çekip kullanýyorum
            var profiles = ProfileHelper.GetProfiles();
            //profiller elimde modelin profilini profillere ekleyeceðim.
            profiles.Add(new UserCreateModelProfile());
            var configuration = new MapperConfiguration(opt=>
            {
                opt.AddProfiles(profiles);
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
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

            app.UseRouting();
            //custom Aythentication için (giriþ iþlemleri)
            app.UseAuthentication();
            app.UseAuthorization();
            //auto route
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
