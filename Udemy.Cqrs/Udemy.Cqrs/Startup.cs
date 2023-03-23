using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Udemy.Cqrs.CQRS.Handlers;
using Udemy.Cqrs.data;

namespace Udemy.Cqrs
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //context baðlantýsý
            services.AddDbContext<StudentContext>(opt =>
            {
                opt.UseSqlServer("server=DESKTOP-4UF23CE;database=StudentDb;integrated security=true;");
            });

            //mediator pattern için ne tarafta çalýþacaðýný belirtiyoruz.
            //injectionlarý scoplarý kaldýrýyoruz bunu ekledikten sonra
            services.AddMediatR(typeof(Startup));

            //classa baðlý kalmamak için  artýk ben bunu dependency injection aracýlýðýyla ele alabileceðim.
            //services.AddScoped<GetStudentByIdQueryHandler>();
            //services.AddScoped<GetStudentsQueryHandler>();
            //services.AddScoped<CreateStudentCommandHandler>();
            //services.AddScoped<RemoveStudentCommandHandler>();
            //services.AddScoped<UpdateStudentCommandHandler>();

            //projeye sadece Controller'larý dahil etmek apilerdeki gibi.
            //ReferanceLoopHangling denilen olaydan kaçmak için Microsoft.NetCore.NewtonSoft ekleyelim projeye
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                //bir enum üzerinden Ignore'u seçiyoruz ve ReferenceLoopHandling olayýndan kurtuluyoruz.
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
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
                //route olayýný contorellers'tan alsýn ayný apilerdeki gibi.
                endpoints.MapControllers();
            });
        }
    }
}
