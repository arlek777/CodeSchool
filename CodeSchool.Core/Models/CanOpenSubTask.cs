using System.Collections.Generic;
using PassJs.DomainModels;

namespace PassJs.Core.Models
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