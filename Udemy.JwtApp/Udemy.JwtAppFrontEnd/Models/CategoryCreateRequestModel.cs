using System.ComponentModel.DataAnnotations;

namespace Udemy.JwtAppFrontEnd.Models
{
    public class CategoryCreateRequestModel
    {
        [Required(ErrorMessage = "Kategori Adı Boş Olamaz.")]
        public string? Definition { get; set; }
    }
}
