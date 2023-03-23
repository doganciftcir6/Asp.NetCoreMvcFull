using System;

namespace Udemy.WebAPI.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ImagePath { get; set; } = null;
        //ilişki
        public int? CategorId { get; set; }
        public Category Category { get; set; }
    }
}
