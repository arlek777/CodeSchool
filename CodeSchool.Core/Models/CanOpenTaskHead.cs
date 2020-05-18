using System.Collections.Generic;
using PassJs.DomainModels;

namespace PassJs.Core.Models
{
    internal class CanOpenTaskHead
    {
        public bool CanOpen { get; set; }
        public int UserTaskHeadId { get; set; }
        public User User { get; set; }
        public ICollection<UserTaskHead> UserTaskHeads { get; set; }
    }
}