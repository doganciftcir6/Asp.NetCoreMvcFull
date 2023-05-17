namespace Udemy.JwtApp.BackOffice.Core.Domain
{
    public class AppRole
    {
        public int Id { get; set; }
        public string? Defination { get; set; }
        //appuser ilişkisi
        public List<AppUser>? AppUsers { get; set; }
    }
}
