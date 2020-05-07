using System.Collections.Generic;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Models
{
    internal class CanOpenTaskHead
    {
        public bool CanOpen { get; set; }
        public int UserTaskHeadId { get; set; }
        public User User { get; set; }
        public ICollection<UserTaskHead> UserTaskHeads { get; set; }
    }
}