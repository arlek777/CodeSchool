using System;

namespace PassJs.Web.Models.SubTasks
{
    public class CanOpenSubTaskModel
    {
        public Guid UserId { get; set; }
        public int UserTaskHeadId { get; set; }
        public int UserSubTaskId { get; set; }
    }
}