using System;
using System.Collections.Generic;
using System.Linq;
using CodeSchool.Web.Models.SubTasks;

namespace CodeSchool.Web.Models.TaskHeads
{
    public class UserTaskHeadShortcutModel
    {
        public UserTaskHeadShortcutModel()
        {
            UserSubTasks = new List<UserSubTaskShortcutModel>();
        }

        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int TaskHeadId { get; set; }
        public bool IsPassed { get; set; }
        public string TaskHeadTitle { get; set; }
        public int TaskHeadOrder { get; set; }

        public UserSubTaskShortcutModel FirstSubTask
        {
            get { return UserSubTasks.FirstOrDefault();  }
        }
        public List<UserSubTaskShortcutModel> UserSubTasks { get; set; }
    }
}