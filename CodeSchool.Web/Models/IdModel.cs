using CodeSchool.Web.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models
{
    public class IdModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int Id { get; set; }
    }
}