namespace Udemy.JwtAppFrontEnd.Models
{
    public class JwtResponseModel
    {
        public string? Token { get; set; }
        //ayrıca ben frontend tarafında token süresi bilgsiini de görmek istiyorum o yüzden
        public DateTime ExpireDate { get; set; }
    }
}
