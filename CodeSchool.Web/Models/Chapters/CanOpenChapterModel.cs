using System;

namespace CodeSchool.Web.Models.TaskHeads
{
    public class CanOpenTaskHeadModel
    {
        public Guid UserId { get; set; }
        public int UserTaskHeadId { get; set; }
    }
}