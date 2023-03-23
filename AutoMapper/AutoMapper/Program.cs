using System;

namespace AutoMapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new MapperConfiguration(opt =>
            {
                opt.CreateMap<Product, ProductListDto>();
                opt.CreateMap<ProductListDto, Product>();
            });
            var mapper = configuration.CreateMapper();
            var dto = mapper.Map<ProductListDto>(new Product
            {
                Id = 1,
                Name = "Telefon",
                Stock = 10
            });
            mapper.Map<Product>(dto);

            Console.WriteLine("Hello World!");
        }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
    }
    public class ProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
    }
}
