using System.Collections.Generic;
using System.Linq;

namespace Udemy.BankApp.Web.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class, new()
    {
        //create
        void Create(T entity);
        //remove
        void Remove(T entity);
        //getall
        List<T> GetAll();
        //getone
        T GetById(object id);
        //update
        void Update(T entity);
        //kullanıcı bilgisi gösterme
        IQueryable<T> GetQueryable();


    }
}
