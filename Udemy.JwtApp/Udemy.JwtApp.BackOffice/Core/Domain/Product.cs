namespace Udemy.JwtApp.BackOffice.Core.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        //Category tablosu ilişkisi
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
