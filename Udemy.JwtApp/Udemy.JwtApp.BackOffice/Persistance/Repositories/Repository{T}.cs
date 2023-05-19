using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Udemy.JwtApp.BackOffice.Core.Application.Interfaces;
using Udemy.JwtApp.BackOffice.Persistance.Context;

namespace Udemy.JwtApp.BackOffice.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly UdemyJwtContext _context;

        public Repository(UdemyJwtContext context)
        {
            _context = context;
        }



        public async Task CreateAsync(T entity)
        {
            //normalde _context.Entity şeklinde ilerlenir ama generic olduğu için .Set<T> diyoruz yani burda bir nevi entity'i seçmiş onun üzerinden işlem yapmış gibi oluyoruz sadece generice uyarlıyoruz.
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            //geriye Set'in içine gelen entity'i liste olarak dönecek mantığı bu. 
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            //bu trackingli bir metot olacak
            //FindAsync metotunda ilgili değer nullable geliyor ondan ? ekleyelim.
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
