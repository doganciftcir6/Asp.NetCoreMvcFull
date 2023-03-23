using Microsoft.EntityFrameworkCore;
using Udemy.BankApp.Web.Data.Configurations;
using Udemy.BankApp.Web.Data.Entities;

namespace Udemy.BankApp.Web.Data.Context
{
    public class BankContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //kanstraktır
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {

        }
        //Configure class haberlemesi
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
