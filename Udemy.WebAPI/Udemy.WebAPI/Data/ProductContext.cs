using Microsoft.EntityFrameworkCore;
using System;

namespace Udemy.WebAPI.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        //tablolara data eklemesi yapalım tablo bu datalar ile ayaklansın (SeedData).
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new(){Id = 1, Name="Elektronik"},
                new(){Id = 2, Name="Giyim"},
            });

            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new(){Id=1,Name="Bilgisayar",CreatedDate = DateTime.Now.AddDays(-3),Price=15000,Stock=300,CategorId= 1},
                new(){Id=2,Name="Telefon",CreatedDate = DateTime.Now.AddDays(-30),Price=10000,Stock=500,CategorId= 1},
                new(){Id=3,Name="Klavye",CreatedDate = DateTime.Now.AddDays(-60),Price=100,Stock=1000,CategorId= 1}
            });
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
