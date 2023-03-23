using System.ComponentModel.DataAnnotations;

namespace Udemy.Identity.Models
{
    public class UserAdminCreateModel
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email adresi gereklidir.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Cinsiyet gereklidir.")]
        public string Gender { get; set; }
    }
}
