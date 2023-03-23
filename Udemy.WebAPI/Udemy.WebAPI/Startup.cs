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
            //Jason WEB TOKEN KONFÝGURASYONU
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                //Bunun defalut ayarý true'dýr yani https ile çalýþýr ama bizim projemiz http o yüzden false çektim.
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    //token'ý üreten
                    ValidIssuer = "http://localhost",
                    //token'ý kullanacak olan
                    ValidAudience = "http://localhost",
                    //bizden securitykey istiyor
                    //istediði byte'ý base64 e çevirip vermem gerekiyor. Daha sonra þifremi veriyorum.
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Yavuzyavuzyavuz1.")),
                    //sen benim þifremi keyimi mutlaka validate et diyoruz.
                    ValidateIssuerSigningKey = true,
                    //Bþlangýç deðerini doðrulamak token'ý kontrol edecek tokenýn zamaný geçmiþ mi geçemmiþ mi
                    ValidateLifetime = true,
                    //sucunu ile client arasýnda oluþabilecek zaman farklýlýklarýný düþünerek default olarak belli gecikme süresi atanýr bunun önüne geçmek için böyle deriz gecikme süresi olmaz.
                    ClockSkew = TimeSpan.Zero,


                };
            });


            //context normalde bu businness tarafýnda olmalýydý. Ama API'de bu zorunluluk yok.
            //connactionStringi buraya deðilde appsettings.json'a yazalým.
            services.AddDbContext<ProductContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Local"));
            });

            //dependencyInjection çalýþmasý için Scopes.
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IDummyRepository,DummyRepository>();

            //CORS
            services.AddCors(cors =>
            {
                cors.AddPolicy("UdemyCorsePolicy", opt =>
                {
                    //her türlü orign'den gelen isteði kabuþ edecek.(google.com, facebook.com gibi)
                    //her türlü header'ý kabul edecek (applicationjason olabilir text olabilir html olabilir)
                    //her türlü metotu kabul edecek
                    //sadece belirli sitelere açmak istersek .WirhOrigins kullanabiliriz. Ýlgili adreslere api'ý açarýz sadece
                    opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            //Controller ve NewtonSoft paketinin kullanýmý.
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
