using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSchool.Domain
{
    public class UserLesson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int LessonId { get; set; }
        public Guid UserId { get; set; }
        public Guid UserChapterId { get; set; }
        public bool IsPassed { get; set; }
        public string Code { get; set; }
        public DateTime UpdatedDt { get; set; }
        
        public virtual User User { get; set; }
        public virtual UserChapter UserChapter { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}