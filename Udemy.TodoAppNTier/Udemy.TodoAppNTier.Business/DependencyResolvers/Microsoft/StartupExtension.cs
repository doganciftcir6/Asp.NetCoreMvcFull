using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Business.Interfaces;
using Udemy.TodoAppNTier.Business.Mapping.AutoMapper;
using Udemy.TodoAppNTier.Business.Services;
using Udemy.TodoAppNTier.Business.ValidationRules;
using Udemy.TodoAppNTier.DataAccess.Contexts;
using Udemy.TodoAppNTier.DataAccess.UnitOfWork;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTier.Entities.Concrete;

namespace Udemy.TodoAppNTier.Business.DependencyResolvers.Microsoft
{
    //Buradaki oluşturacağımız class içerisinde StartUp dosyası içindeki IserviceCollactionu genişleticez. Yani contextin ilgili ayarlamalarını bağlantılarını burda yapmış oluruz.
    public static class StartupExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            //AutoMapper
            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new WorkProfile());
            });
            var mapper = configuration.CreateMapper();

            //mapper'ı dependency injeciton ile ele almak
            services.AddSingleton(mapper);

            //FluentValidation'ı dependenct injection ile ele almak
            services.AddTransient<IValidator<WorkCreateDto>, WorkCreateDtoValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>, WorkUpdateValidator>();

            //uow
            services.AddScoped<IUow, Uow>();
            //businness services
            services.AddScoped<IWorkService, WorkService>();


            services.AddDbContext <TodoContext> (opt =>
            {
                opt.UseSqlServer("server=DESKTOP-4UF23CE; database=TodoDb; integrated security=true;");
                opt.LogTo(Console.WriteLine, LogLevel.Information);
            });
        }
    }
}
