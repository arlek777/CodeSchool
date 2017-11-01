using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models
{
    public class RegistrationModel
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Password { get; set; }
    }
}