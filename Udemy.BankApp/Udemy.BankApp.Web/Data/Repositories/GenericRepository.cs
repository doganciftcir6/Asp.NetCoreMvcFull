using System.Collections.Generic;
using System.Linq;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Interfaces;

namespace Udemy.BankApp.Web.Data.Repositories
{
    public class GenericRepository<T> :IGenericRepository<T> where T : class, new()
    {
        //tekrarlı metotları yazarız.

        //contexti kullanabilmek için dependency injection
        private readonly BankContext _context;

        public GenericRepository(BankContext context)
        {
            _context = context;
        }

        //create
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        //remove
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        //getall
        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        //getone
        public T GetById(object id)
        {
            return _context.Set<T>().Find(id);
        }
        
        //update
        public void Update(T entity) 
        {
            _context.Set<T>().Update(entity);
        }
        //database sorguya açmak
        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
