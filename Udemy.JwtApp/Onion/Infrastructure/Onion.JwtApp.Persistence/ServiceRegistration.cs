using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onion.JwtApp.Application.Interfaces;
using Onion.JwtApp.Persistence.Context;
using Onion.JwtApp.Persistence.Repositories;

namespace Onion.JwtApp.Persistence
{
    public static class ServiceRegistration
    {
        //bir classı veya özelliği this ile genişletebiliyoruz servicesi genişleticez. Ayrıca connectionstring bilgisini bir envrominent içinden çekmek için configurationu genişeticez.
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services e yeni özellikler kazandırabiliriz
            services.AddDbContext<JwtContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Local"));
            });
            //scopes
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
