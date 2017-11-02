using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models.Lessons
{
    public class UserLessonShortcutModel
    {
        public Guid Id { get; set; }
        public int LessonId { get; set; }
        public Guid UserChapterId { get; set; }
        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }
        public string LessonTitle { get; set; }
        public int LessonOrder { get; set; }
    }

    public class UserLessonModel: UserLessonShortcutModel
    {
        [Required]
        [StringLength(5000)]
        public string Code { get; set; }
        public LessonModel Lesson { get; set; }
    }
}