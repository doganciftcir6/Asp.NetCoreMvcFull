namespace Udemy.JwtApp.BackOffice.Infrastructure.Tools
{
    //TOKEN BİLGİLERİNİ TUTACAĞIMIZ YER AYARLAMALARINI YAPACAĞIMIZ YER.
    public class JwtTokenSettings
    {
        //ASLINDA PROGRAM CLASSI TARAFINDA AYARLAMALIRIMIZ BUNLARI BİR DEĞİŞKENE ATALIM
        //ValidAudience = "http://localhost",
        //ValidIssuer = "http://localhost",
        //ClockSkew = TimeSpan.Zero,
        //ValidateLifetime = true,
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yavuzyavuzyavuz1.")),
        //ValidateIssuerSigningKey = true

        public const string Issuer = "http://localhost";
        public const string Audience = "http://localhost";
        public const string Key = "yavuzyavuzyavuz1.";
        public const int Expire = 30;
    }
}
