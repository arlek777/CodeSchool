using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models
{
    public class RegistrationRequestModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(50, ErrorMessage = ValidationResultMessages.MaxLength)]
        [EmailAddress(ErrorMessage = ValidationResultMessages.EmailRequiredOrInvalid)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(30, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(30, ErrorMessage = ValidationResultMessages.MaxLength)]
        [MinLength(5, ErrorMessage = ValidationResultMessages.PasswordShort)]
        public string Password { get; set; }
    }
}