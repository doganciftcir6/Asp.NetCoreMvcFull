using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Onion.JwtApp.Application.Mappings;
using System.Reflection;

namespace Onion.JwtApp.Application
{
    //bazı paketler kurduk automapper gibi bunların ayarları ve registerleri burda olacak
    //this ifadesi ile genişletme yapıcaz. Servicesi genişeticez.
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            //GetExecutingAssembly çalıştığı yere al.
            //MediatR pattern hangi querye karşılık hangi handler ı çalıştıracağımızı interfaceler aracılığıyla belirleyen patterndır.
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(opt =>
            {
                opt.AddProfiles(new List<Profile>
                {
                    //automapper profilleri buraya eklicez.
                    new CategoryProfile(),
                    new ProductProfile(),
                });
            });
        }
    }
}
