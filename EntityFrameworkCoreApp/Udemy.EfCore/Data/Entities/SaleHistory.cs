namespace Udemy.EfCore.Data.Entities
{
    //ONE TO MANY İLİŞKİ PRODUCT TABLOSU İLE
    public class SaleHistory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        //ProductId kolonu aslında bir Product’ı göstermesi gerekiyor. 
        //NAVİGATİON PROPERTY
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
