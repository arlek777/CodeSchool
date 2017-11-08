using CodeSchool.Web.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models
{
    public class RemoveRequestModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int Id { get; set; }
    }
}