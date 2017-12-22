using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models
{
    public class ChangeOrderModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int CurrentId { get; set; }
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int ToSwapId { get; set; }
    }
}