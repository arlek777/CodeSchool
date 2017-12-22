using System.Collections.Generic;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Models
{
    internal class CanOpenChapter
    {
        public bool CanOpen { get; set; }
        public int UserChapterId { get; set; }
        public User User { get; set; }
        public ICollection<UserChapter> UserChapters { get; set; }
    }
}