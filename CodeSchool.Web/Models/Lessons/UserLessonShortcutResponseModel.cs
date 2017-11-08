using System;

namespace CodeSchool.Web.Models.Lessons
{
    public class UserLessonShortcutResponseModel
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int UserChapterId { get; set; }
        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }
        public string LessonTitle { get; set; }
        public int LessonOrder { get; set; }
    }
}