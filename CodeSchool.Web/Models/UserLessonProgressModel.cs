using System;
using CodeSchool.Domain;

namespace CodeSchool.Web.Models
{
    public class UserLessonModel
    {
        public UserLessonModel()
        {
        }

        public UserLessonModel(UserLesson model)
        {
            Id = model.Id;
            UserId = model.UserId;
            LessonId = model.LessonId;
            IsPassed = model.IsPassed;
            UserChapterId = model.UserChapterId;
            Code = model.Code;
            UpdatedDt = model.UpdatedDt;
        }

        public Guid Id { get; set; }
        public int LessonId { get; set; }
        public Guid UserChapterId { get; set; }
        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }
        public string Code { get; set; }
        public DateTime UpdatedDt { get; set; }
    }
}