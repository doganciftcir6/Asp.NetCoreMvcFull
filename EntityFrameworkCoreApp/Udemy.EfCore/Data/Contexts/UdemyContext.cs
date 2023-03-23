using Microsoft.EntityFrameworkCore;
using Udemy.EfCore.Data.Entities;

namespace Udemy.EfCore.Data.Contexts
{
    public class UdemyContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SaleHistory> saleHistories { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PartTimeEmployee> PartTimeEmployees { get; set; }
        public DbSet<FullTimeEmployee> FullTimeEmployees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-4UF23CE; database=UdemyEfCore; integrated security=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //table per type
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<FullTimeEmployee>().ToTable("FullTimeEmployees");
            modelBuilder.Entity<PartTimeEmployee>().ToTable("PartTimeEmployees");

            //many to many
            modelBuilder.Entity<ProductCategory>().HasKey(x => new { x.CategoryId, x.ProductId });
            modelBuilder.Entity<Product>().HasMany(x => x.ProductCategories).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Category>().HasMany(x => x.ProductCategories).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);

            //one to many
            //modelBuilder.Entity<Product>().HasMany(X=> X.SaleHistories).WithOne(X=> X.Product).HasForeignKey(X=>X.ProductId);
            modelBuilder.Entity<SaleHistory>().HasOne(X=> X.Product).WithMany(x=> x.SaleHistories).HasForeignKey(X=>X.ProductId);

            //one to one
            modelBuilder.Entity<Product>().HasOne(x => x.ProductDetail).WithOne(x => x.Product).HasForeignKey<ProductDetail>(x => x.ProductId);


            //modelBuilder.Entity<Customer>().HasNoKey();
            modelBuilder.Entity<Customer>().HasKey(x => new {x.Number,x.Name});
            //modelBuilder.Entity<Category>().ToTable(name: "Categories", schema: "dbo");

            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("product_name");
            modelBuilder.Entity<Product>().Property(x => x.Name).HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired(true);
            //modelBuilder.Entity<Product>().Property(x => x.Name).HasDefaultValueSql("'Urun bilgisi girilmemis'");
            modelBuilder.Entity<Product>().HasIndex(x => x.Name).IsUnique(true);
            modelBuilder.Entity<Product>().Property(x => x.CreatedTime).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("product_id");

            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnName("product_price");
            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 3);

            base.OnModelCreating(modelBuilder);
        }
    }
}
