using CodeSchool.Web.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models
{
    public class IdRequestModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int Id { get; set; }
    }
}