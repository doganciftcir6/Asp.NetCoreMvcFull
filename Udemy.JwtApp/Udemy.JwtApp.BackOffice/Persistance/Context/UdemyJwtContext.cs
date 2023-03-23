using Microsoft.EntityFrameworkCore;
using Udemy.JwtApp.BackOffice.Core.Domain;
using Udemy.JwtApp.BackOffice.Persistance.Configurations;

namespace Udemy.JwtApp.BackOffice.Persistance.Context
{
    public class UdemyJwtContext : DbContext
    {
        //optionu base'e gönderelim
        public UdemyJwtContext(DbContextOptions<UdemyJwtContext> options) : base(options)
        {

        }
        //dbsetleri oluştururken .netcore6.0 ile null değerlere karşı önlemler arttı biraz daha korunmamız gerekiyor. 
        //bunların setini açmak çok doğru bir yaklaşım değil sonuç olarak ben Product'ı gelip setlemicem ben bu veritabanındaki verileri getiricem bir şekilde yani setter'ının açık olması çok mantıklı değil DbSet<Product>? yazarak bu önlemden kurtulabiliriz veya aşağıdaki gibi bir işlem yapıp setter'ı kapatıp null önlemlerden kurtulabiliriz.
        public DbSet<Product> Products => this.Set<Product>();
        public DbSet<Category> Categories => this.Set<Category>();
        public DbSet<AppUser> AppUsers => this.Set<AppUser>();
        //yukarıdaki kullanımların yaptığı şey aşağıdakiyle aynıdır setter'ı kapatır sadece kısayol.
        public DbSet<AppRole> AppRoles
        {
            get
            {
                return this.Set<AppRole>();
            }
        }

        //override edelim ve modelBuilder üzerinden configurationslarımızı uygulayalım
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Persistance klasöründe Configurations klasöründe yaptığımız configurationları burada contexte haber edelim.
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
