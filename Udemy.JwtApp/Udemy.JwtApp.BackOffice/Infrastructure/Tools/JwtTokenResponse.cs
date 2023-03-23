namespace Udemy.JwtApp.BackOffice.Infrastructure.Tools
{
    public class JwtTokenResponse
    {

        //doğrudan ilgili datayı dönmek yerine bu şekilde hareket edelim. Bu sayede Token: "token" şeklinde obje rotasyonuyla bir sonuç alıcaz swagger'da
        public string Token { get; set; }
        //ayrıca ben frontend tarafında token süresi bilgsiini de görmek istiyorum o yüzden
        public DateTime ExpireDate { get; set; }
        //dışarıdan gelen token bilgisini burdaki prop olan Token'a atalım.
        public JwtTokenResponse(string token, DateTime expireDate)
        {
            Token = token;
            ExpireDate = expireDate;
        }
    }
}
