using System;
using System.Collections.Generic;

namespace Udemy.EfCore.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //nav prop one to many - SaleHistroy tablosu arasında
        public List<SaleHistory> SaleHistories { get; set; }
        //nav prop one to one - ProductDetail tablosu arasında
        public ProductDetail ProductDetail { get; set; }
        //many to many - Category tablosu arasında - nav prop
        public List<ProductCategory> ProductCategories { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
