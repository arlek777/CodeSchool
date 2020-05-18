using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PassJs.DomainModels
{
    public enum SubTaskType
    {
        Code = 0,
        Test,
        LongAnswer
    }

    public enum SubTaskLevel
    {
        Junior = 0,
        Middle,
        Senior
    }

    public class SubTask
    {
        public SubTask()
        {
            AnswerSubTaskOptions = new List<AnswerSubTaskOption>();
            UserSubTasks = new List<UserSubTask>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int TaskHeadId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        public string Text { get; set; }

        public string Answer { get; set; }

        public string TaskText { get; set; }

        public string UnitTestsCode { get; set; }

        public string ReporterCode { get; set; }

        public SubTaskType Type { get; set; }

        public SubTaskLevel Level { get; set; }

        public int Order { get; set; }

        public bool Published { get; set; }

        public virtual TaskHead TaskHead { get; set; }

        public virtual ICollection<UserSubTask> UserSubTasks { get; set; }

        public virtual ICollection<AnswerSubTaskOption> AnswerSubTaskOptions { get; set; }
    }
}
