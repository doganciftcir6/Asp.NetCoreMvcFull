using System.Collections.Generic;
using System.Threading.Tasks;
using Udemy.WebAPI.Data;

namespace Udemy.WebAPI.Interfaces
{
    //veritabanı ile ilgili işlemlerimizi yaparken burdan faydalanalım direkt contexti örneklemeyelim.
    public interface IProductRepository
    {
        //tüm veriyi getiren metot
        Task<List<Product>> GetAllAsync();
        //ıd ile veri getiren metot
        Task<Product> GetByIdAsync(int id);
        //create işlemi yapan metot
        Task<Product> CreateAsync(Product product);
        //update işlemi yapan metot
        Task UpdateAsync(Product product);
        //remove işlemi yapan metot
        Task DeleteAsync(int id);

    }
}
