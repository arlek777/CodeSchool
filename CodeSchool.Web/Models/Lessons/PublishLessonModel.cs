using System;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.SubTasks
{
    public class PublishSubTaskModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int SubTaskId { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int TaskHeadId { get; set; }
    }
}
