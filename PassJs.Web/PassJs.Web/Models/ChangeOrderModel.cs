using System;
using System.ComponentModel.DataAnnotations;

namespace PassJs.Web.Models
{
    public enum Test
    {
        One = 0,
        Two
    }

    public class ChangeOrderModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int CurrentId { get; set; }
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int ToSwapId { get; set; }
    }
}