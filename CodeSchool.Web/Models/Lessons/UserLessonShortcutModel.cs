using System;

namespace CodeSchool.Web.Models.SubTasks
{
    public class UserSubTaskShortcutModel
    {
        public int Id { get; set; }
        public int SubTaskId { get; set; }
        public int UserTaskHeadId { get; set; }
        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }
        public string SubTaskTitle { get; set; }
        public int SubTaskOrder { get; set; }
    }
}