namespace Udemy.JwtApp.BackOffice.Core.Domain
{
    public class AppRole
    {
        public int Id { get; set; }
        public string? Defination { get; set; }
        //appuser ilişkisi
        public List<AppUser> AppUsers { get; set; }
        //eğer bir role geldiyse sen bunu boş olarak en kötü ihtimalle örnekle null bırakma diyoruz.
        public AppRole()
        {
            AppUsers = new List<AppUser>();
        }
    }
}
