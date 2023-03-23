using Microsoft.AspNetCore.Identity;

namespace Udemy.Identity.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string ImagePath { get; set; }
        //3NF ye aykırı
        public string Gender { get; set; }
    }
}
