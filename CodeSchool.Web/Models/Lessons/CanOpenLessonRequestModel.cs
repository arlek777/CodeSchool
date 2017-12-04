using System;

namespace CodeSchool.Web.Models.Lessons
{
    public class CanOpenLessonRequestModel
    {
        public Guid UserId { get; set; }
        public int UserChapterId { get; set; }
        public int UserLessonId { get; set; }
    }
}