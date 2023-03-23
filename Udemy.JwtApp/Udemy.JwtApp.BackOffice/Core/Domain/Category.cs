namespace Udemy.JwtApp.BackOffice.Core.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string? Definition { get; set; }
        //Product tablosu ilişkisi
        public List<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}
