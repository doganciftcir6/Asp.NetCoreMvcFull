using System.Linq.Expressions;

namespace Udemy.JwtApp.BackOffice.Core.Application.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
        //bu GetByIdAsync metotunun id değeriyle ilgili kaydı asnotracking li halini veya asnotrackingsiz getiriyor olabilir mesela bir update için veya bir ürünün sadece detayını göstermek için kullanıyor olabilir. Bu noktada GetByFilter'I kullanabiliriz.
        //Expression ile parametre içerisinde delege yani x=> şeklinde lambda expression geçilmesini sağlıyorum mantığı bu oradaki T sayesinde x.Entityinidsi şeklinde tanımlama yapabiliyorum bool ise oradaki işlemin true false olup olmadığını belirtiyor yani x.entityİdsi == (eşitmi) request.Id  dışarıdan gelen ıd'ye eşitse true değilse false döndürüyor.
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}
