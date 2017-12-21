using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain.Lessons
{
    public class UserLesson
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int LessonId { get; set; }

        [Required]
        public int UserChapterId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public bool IsPassed { get; set; }
        public string Code { get; set; }
        public DateTime UpdatedDt { get; set; }
        
        public virtual User User { get; set; }
        public virtual UserChapter UserChapter { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}