using System;
using System.Collections.Generic;

namespace CodeSchool.Domain.Lessons
{
    public class UserChapter
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ChapterId { get; set; }
        public bool IsPassed { get; set; }

        public virtual User User { get; set; }
        public virtual Chapter Chapter { get; set; }
        public virtual ICollection<UserLesson> UserLessons { get; set; }
    }
}