using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.DataAccess.Configurations;
using Udemy.TodoAppNTier.Entities.Concrete;

namespace Udemy.TodoAppNTier.DataAccess.Contexts
{
    public class TodoContext : DbContext
    {
        //kanstraktır çünkü Contexti başka bir yerde kullanırken Dependency İnjection ile kullanıcam.
        //Context bağlantısını startup'ta gerçekleştirmicez çünkü UI doğrudan DataAccess katmanını görmemesi gerekiyor.Bu işi Businnes katmanına 
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
        //configuration klasörüden contexti haberdar etmek
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkConfiguration());
        }
        public DbSet<Work> Works { get; set; }
    }
}
