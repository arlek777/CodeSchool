using System.ComponentModel.DataAnnotations;

namespace PassJs.Web.Models
{
    public class IdModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int Id { get; set; }
    }
}