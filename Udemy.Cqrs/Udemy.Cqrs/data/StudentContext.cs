using Microsoft.EntityFrameworkCore;

namespace Udemy.Cqrs.data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //db belli datalarla ayaklansın
            modelBuilder.Entity<Student>().HasData(new Student[]
            {
                new Student() {Name = "Yavuz", Age = 29, Surname = "Kahraman", Id = 1},
                new Student() {Name = "Mehmet", Age = 25, Surname = "Kahraman", Id = 2},
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
