using Udemy.BankApp.Web.Data.Interfaces;

namespace Udemy.BankApp.Web.Data.UnitOfWork
{
    public interface IUow
    {
        IGenericRepository<T> GetRepository<T>() where T : class, new();
        void SaveChanges();
    }
}