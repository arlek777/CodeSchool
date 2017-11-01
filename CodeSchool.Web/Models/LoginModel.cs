using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }
    }
}