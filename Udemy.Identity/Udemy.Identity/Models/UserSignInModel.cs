using System.ComponentModel.DataAnnotations;

namespace Udemy.Identity.Models
{
    //Giriş yaparken nelere ihtiyacım var?
    public class UserSignInModel
    {
        [Required(ErrorMessage ="Kullanıcı adı gereklidir.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Parola alanı gereklidir.")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
