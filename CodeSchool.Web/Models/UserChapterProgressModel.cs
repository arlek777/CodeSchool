using System;
using System.Collections.Generic;
using System.Linq;
using CodeSchool.Domain;

namespace CodeSchool.Web.Models
{
    public class UserChapterProgressModel
    {
        public UserChapterProgressModel()
        {
            
        }

        public UserChapterProgressModel(UserChapterProgress model)
        {
            Id = model.Id;
            UserId = model.UserId;
            ChapterId = model.ChapterId;
            IsPassed = model.IsPassed;
            UserLessonProgresses = model.UserLessonProgresses.Select(l => new UserLessonProgressModel(l));
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int ChapterId { get; set; }
        public bool IsPassed { get; set; }
        public IEnumerable<UserLessonProgressModel> UserLessonProgresses { get; set; }
    }
}