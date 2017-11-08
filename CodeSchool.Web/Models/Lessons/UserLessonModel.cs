using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models.Lessons
{
    public class UserLessonShortcutModel
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int UserChapterId { get; set; }
        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }
        public string LessonTitle { get; set; }
        public int LessonOrder { get; set; }
    }

    public class UserLessonModel: UserLessonShortcutModel
    {
        public string Code { get; set; }
        public LessonModel Lesson { get; set; }
    }
}