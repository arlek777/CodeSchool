using System;
using System.Collections.Generic;

namespace CodeSchool.Domain
{
    public class UserChapter
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ChapterId { get; set; }

        public bool IsPassed { get; set; }

        public DateTime CreatedDt { get; set; }

        public DateTime? StartedDt { get; set; }

        public DateTime? UpdatedDt { get; set; }
        
        public DateTime? FinishedDt { get; set; }

        public double TaskDurationLimit { get; set; }

        public int UnfocusCount { get; set; }

        public int CopyPasteCount { get; set; }

        public virtual User User { get; set; }
        public virtual Chapter Chapter { get; set; }
        public virtual ICollection<UserLesson> UserLessons { get; set; }
    }
}