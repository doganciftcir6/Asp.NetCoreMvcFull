using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Interfaces;
using Udemy.BankApp.Web.Data.Repositories;

namespace Udemy.BankApp.Web.Data.UnitOfWork
{
    public class Uow : IUow
    {
        //dependency injection
        private readonly BankContext _context;

        public Uow(BankContext context)
        {
            _context = context;
        }

        //bir metot yazıcaz
        public IGenericRepository<T> GetRepository<T>() where T:class,new()
        {
            return new GenericRepository<T>(_context);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
