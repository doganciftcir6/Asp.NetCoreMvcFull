using System.Collections.Generic;

namespace Udemy.WebAPI.Data
{
    //bir category'de birden fazla product olabilir ilişkisi kurulacak.
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
