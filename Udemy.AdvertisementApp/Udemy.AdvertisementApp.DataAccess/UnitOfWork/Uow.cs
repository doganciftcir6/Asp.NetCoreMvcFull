using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.AdvertisementApp.DataAccess.Contexts;
using Udemy.AdvertisementApp.DataAccess.Interfaces;
using Udemy.AdvertisementApp.DataAccess.Repositories;
using Udemy.AdvertisementApp.Entities;

namespace Udemy.AdvertisementApp.DataAccess.UnitOfWork
{
    //ilgili repositoryleri tek bir context'in ilgili requeste gittiğinden emin olmak
    public class Uow : IUow
    {
        //context
        private readonly AdvertisementAppContext _context;

        public Uow(AdvertisementAppContext context)
        {
            _context = context;
        }
        //2 metot
        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            //dependency'den gelen contexti repository'e göndermek.
            return new Repository<T>(_context);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
