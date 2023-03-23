namespace Udemy.EfCore.Data.Entities
{
    public class ProductCategory
    {
        //many to many ilişki tablosu - Product ve Category tablosu arasında
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
