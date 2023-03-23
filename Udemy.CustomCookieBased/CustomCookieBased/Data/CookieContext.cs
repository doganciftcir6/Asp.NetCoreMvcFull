using CustomCookieBased.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CustomCookieBased.Data
{
    public class CookieContext : DbContext
    {
        //contextimi dependency injection ile kullanabilmek için.
        public CookieContext(DbContextOptions<CookieContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //CONFİGURATİONLARI CONTEXE HABER ETMEK.
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserRoleConfiguration());
        }
        //dbsetler
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUserRole> UserRoles { get; set; }
    }
}
