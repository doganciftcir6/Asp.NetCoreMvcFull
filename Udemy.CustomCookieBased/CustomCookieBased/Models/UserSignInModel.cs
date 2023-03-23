namespace CustomCookieBased.Models
{
    //giriş işleminde kullanılacak model
    public class UserSignInModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
