using System;

namespace CodeSchool.Domain
{
    public class UserLesson
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int UserChapterId { get; set; }

        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }
        public string Code { get; set; }
        public DateTime UpdatedDt { get; set; }
        
        public virtual User User { get; set; }
        public virtual UserChapter UserChapter { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}