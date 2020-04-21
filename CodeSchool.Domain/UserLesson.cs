using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public enum UserLessonAnswerScore
    {
        DontKnow = 0,
        HardToRemember,
        KnowIt
    }

    public class UserLesson
    {
        public UserLesson()
        {
            CodeSnapshots = new List<CodeSnapshot>();
        }

        [Required]
        public int Id { get; set; }

        public int LessonId { get; set; }

        public int UserChapterId { get; set; }

        public Guid UserId { get; set; }

        public int UnfocusCount { get; set; }

        public int CopyPasteCount { get; set; }

        public bool IsPassed { get; set; }

        public string Code { get; set; }

        public DateTime UpdatedDt { get; set; }
        public DateTime SubmittedDt { get; set; }
        public TimeSpan TaskDurationLimit { get; set; }

        // Maybe will be removed in future
        public int? SelectedAnswerOptionId { get; set; }

        public UserLessonAnswerScore? Score { get; set; }
        
        // Virtual properties
        public virtual User User { get; set; }
        public virtual UserChapter UserChapter { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<CodeSnapshot> CodeSnapshots { get; set; }
    }
}