using System.Collections.Generic;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Models
{
    internal class CanOpenSubTask
    {
        public bool CanOpen { get; set; }
        public int UserSubTaskId { get; set; }
        public int UserTaskHeadId { get; set; }
        public User User { get; set; }
        public ICollection<UserSubTask> UserSubTasks { get; set; }
    }
}