using System;

namespace CodeSchool.Web.Models.Lessons
{
    public class UserLessonUpdateModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }
        public string Code { get; set; }
    }
}