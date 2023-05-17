namespace Udemy.JwtApp.BackOffice.Core.Domain
{
    public class AppUser
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        //ROLE TABLOSU İLİŞKİSİ
        public int AppRoleId { get; set; }
        public AppRole? AppRole { get; set; }
    }
}
