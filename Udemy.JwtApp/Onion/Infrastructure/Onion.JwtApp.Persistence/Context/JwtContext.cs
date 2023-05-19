using Microsoft.EntityFrameworkCore;
using Onion.JwtApp.Domain.Entities;
using Onion.JwtApp.Persistence.Configurations;

namespace Onion.JwtApp.Persistence.Context
{
    public class JwtContext : DbContext
    {
        //bu ctor içerisine aldığı optionsu base e göndericek bende bunun sayesinde dependecy injectionla dbcontexi ele alabiliyor olacağım
        //parametre içindeki optionsu programcse gönderip orada ele alacağız.
        public JwtContext(DbContextOptions<JwtContext> options) : base(options)
        {
            
        }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
        //Identitydeki User ile karışmaması için burası önemli. Orda zaten User var.
        public DbSet<AppUser>? AppUsers { get; set; }
        public DbSet<AppRole>? AppRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
