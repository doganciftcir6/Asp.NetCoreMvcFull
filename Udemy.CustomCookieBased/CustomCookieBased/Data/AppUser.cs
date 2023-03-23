using System.Collections.Generic;

namespace CustomCookieBased.Data
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //ilişki çoka çok
        public List<AppUserRole> UserRoles { get; set; }
    }
    public class AppRole
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        //İLİŞKİ çoka çok
        public List<AppUserRole> UserRoles { get; set; }
    }
    public class AppUserRole
    {
        public int UserId { get; set; }
        //nav prop
        public AppUser AppUser { get; set; }
        public int RoleId { get; set; }
        //nav prop
        public AppRole AppRole { get; set; }
    }
}
