using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udemy.WebAPI.Data;
using Udemy.WebAPI.Interfaces;

namespace Udemy.WebAPI.Repositories
{
    //veritabanı ile ilgili işlemlerimizi yaparken burdan faydalanalım direkt contexti örneklemeyelim.
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }
        //tüm veriyi getiren metot
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }
        //ıd ile veri getiren metot
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }
        //create işlemi yapan metot
        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
        //update işlemi yapan metot
        public async Task UpdateAsync(Product product)
        {
            //update yapılırken değişmemiş olan entity'e değişen entity'in değerlerini setleriz.
            //değimemiş olan entity bulmak.
            var unchangedEntity = await _context.Products.FindAsync(product.Id);
            //değişmemiş olan entity'e değişmiş olan verileri ekleriz.
            _context.Entry(unchangedEntity).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();

        }
        //remove işlemi yapan metot
        public async Task DeleteAsync(int id)
        {
            //silinecek olan kayıdı bulalım.
            var removedEntity = await _context.Products.FindAsync(id);
            //daha sonra kayıdı db'den silelim.
            _context.Products.Remove(removedEntity);
            await _context.SaveChangesAsync();
        }
    }
}
