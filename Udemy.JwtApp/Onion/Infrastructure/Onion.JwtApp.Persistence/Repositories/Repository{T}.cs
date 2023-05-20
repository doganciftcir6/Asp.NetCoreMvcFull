using Microsoft.EntityFrameworkCore;
using Onion.JwtApp.Application.Interfaces;
using Onion.JwtApp.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly JwtContext _jwtContext;

        public Repository(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
        }

        public async Task<int> CommitAsync()
        {
            return await _jwtContext.SaveChangesAsync();
        }

        public async Task<T?> CreateAsync(T entity)
        {
            _jwtContext.Set<T>().Add(entity);
            //burda entityin statei added olarak setliyor. Savechangesi çağırdığında da stateine bakıyor eğer added ise gidiyor ilgili ekleme sql sorgusunu çalıştırıyor.
            await _jwtContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            //ef core un tracking özelliğini sadece update ve remove işleminde isteriz burası önemli.
            return await _jwtContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            //sadece belirli koşullara göre bir verinin çekilmesi durumu
            return await _jwtContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            //update veya remove işlemi için bunu kullanıcaz. bu yüzden tracking özelliğini bırakmamız önemli.
            return await _jwtContext.Set<T>().FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            _jwtContext.Set<T>().Remove(entity);
            //entityin statesini deleted olarak işaretliyor.SaveChangesAsync state e bakarak ne yapacağına karar veriyor.
            await _jwtContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _jwtContext.Set<T>().Update(entity);
            //state updated olarak işaretlenir.
            await _jwtContext.SaveChangesAsync();
        }
    }
}
