using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public enum TaskType
    {
        Code = 0,
        Test
    }

    public class TaskHead
    {
        public TaskHead()
        {
            SubTasks = new List<SubTask>();
            UserTaskHeads = new List<UserTaskHead>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        public int Order { get; set; }

        public TaskType Type { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<UserTaskHead> UserTaskHeads { get; set; }
        public virtual ICollection<SubTask> SubTasks { get; set; }
    }
}