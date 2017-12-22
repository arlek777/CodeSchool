using System.Collections.Generic;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Models
{
    internal class CanOpenLesson
    {
        public bool CanOpen { get; set; }
        public int UserLessonId { get; set; }
        public int UserChapterId { get; set; }
        public User User { get; set; }
        public ICollection<UserLesson> UserLessons { get; set; }
    }
}