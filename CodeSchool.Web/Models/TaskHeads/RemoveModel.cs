using System;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.TaskHeads
{
    public class RemoveModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public Guid CompanyId { get; set; }
    }
}