namespace Onion.JwtApp.Domain.Entities
{
    public class AppRole
    {
        public int Id { get; set; }
        public string? Defination { get; set; }
        //appuser ilişkisi
        public List<AppUser>? AppUsers { get; set; }
    }
}
