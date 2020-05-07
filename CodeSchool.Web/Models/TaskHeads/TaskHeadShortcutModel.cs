using System;
using System.Collections.Generic;
using CodeSchool.Web.Models.SubTasks;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Domain;

namespace CodeSchool.Web.Models.TaskHeads
{
    public class TaskHeadShortcutModel
    {
        public TaskHeadShortcutModel()
        {
            SubTasks = new List<SubTaskShortcutModel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(256, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int CategoryId { get; set; }

        public Guid CompanyId { get; set; }

        public int Order { get; set; }

        public TaskType Type { get; set; }

        public IEnumerable<SubTaskShortcutModel> SubTasks { get; set; }
    }
}