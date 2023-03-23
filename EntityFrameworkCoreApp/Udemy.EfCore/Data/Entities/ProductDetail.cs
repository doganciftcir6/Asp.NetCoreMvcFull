namespace Udemy.EfCore.Data.Entities
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        //Product ve ProductDetail arasında birebir ilişki
        public int ProductId { get; set; }
        //nav property
        public Product Product { get; set; }
    }
}
