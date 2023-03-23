using System.ComponentModel.DataAnnotations;

namespace Udemy.Identity.Models
{
    public class RoleCreateModel
    {
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        public string Name { get; set; }
    }
}
