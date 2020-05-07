using System;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Domain;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.SubTasks
{
    public class SubTaskShortcutModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int TaskHeadId { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(256, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Title { get; set; }

        public int Order { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public SubTaskType Type { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public SubTaskLevel Level { get; set; }

        public Guid CompanyId { get; set; }

        public bool Published { get; set; }
    }
}