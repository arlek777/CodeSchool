using System;
using System.ComponentModel.DataAnnotations;

namespace PassJs.Web.Models.TaskHeads
{
    public class RemoveModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public Guid CompanyId { get; set; }
    }
}