using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.WebAPI.Data;
using Udemy.WebAPI.Interfaces;
using Udemy.WebAPI.Repositories;

namespace Udemy.WebAPI
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
            //Jason WEB TOKEN KONF�GURASYONU
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                //Bunun defalut ayar� true'd�r yani https ile �al���r ama bizim projemiz http o y�zden false �ektim.
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    //token'� �reten
                    ValidIssuer = "http://localhost",
                    //token'� kullanacak olan
                    ValidAudience = "http://localhost",
                    //bizden securitykey istiyor
                    //istedi�i byte'� base64 e �evirip vermem gerekiyor. Daha sonra �ifremi veriyorum.
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Yavuzyavuzyavuz1.")),
                    //sen benim �ifremi keyimi mutlaka validate et diyoruz.
                    ValidateIssuerSigningKey = true,
                    //B�lang�� de�erini do�rulamak token'� kontrol edecek token�n zaman� ge�mi� mi ge�emmi� mi
                    ValidateLifetime = true,
                    //sucunu ile client aras�nda olu�abilecek zaman farkl�l�klar�n� d���nerek default olarak belli gecikme s�resi atan�r bunun �n�ne ge�mek i�in b�yle deriz gecikme s�resi olmaz.
                    ClockSkew = TimeSpan.Zero,


                };
            });


            //context normalde bu businness taraf�nda olmal�yd�. Ama API'de bu zorunluluk yok.
            //connactionStringi buraya de�ilde appsettings.json'a yazal�m.
            services.AddDbContext<ProductContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Local"));
            });

            //dependencyInjection �al��mas� i�in Scopes.
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IDummyRepository,DummyRepository>();

            //CORS
            services.AddCors(cors =>
            {
                cors.AddPolicy("UdemyCorsePolicy", opt =>
                {
                    //her t�rl� orign'den gelen iste�i kabu� edecek.(google.com, facebook.com gibi)
                    //her t�rl� header'� kabul edecek (applicationjason olabilir text olabilir html olabilir)
                    //her t�rl� metotu kabul edecek
                    //sadece belirli sitelere a�mak istersek .WirhOrigins kullanabiliriz. �lgili adreslere api'� a�ar�z sadece
                    opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            //Controller ve NewtonSoft paketinin kullan�m�.
            services.AddControllers().AddNewtonsoftJson(opt=>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Udemy.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Udemy.WebAPI v1"));
            }

            //WWWROOT
            app.UseStaticFiles();

            app.UseRouting();

            //CORS
            app.UseCors("UdemyCorsePolicy");

            //Jason Web TOken
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
