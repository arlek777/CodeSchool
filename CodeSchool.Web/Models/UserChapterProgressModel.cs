using System;
using System.Collections.Generic;
using System.Linq;
using CodeSchool.Domain;

namespace CodeSchool.Web.Models
{
    public class UserChapterModel
    {
        public UserChapterModel()
        {
            
        }

        public UserChapterModel(UserChapter model)
        {
            Id = model.Id;
            UserId = model.UserId;
            ChapterId = model.ChapterId;
            IsPassed = model.IsPassed;
            UserLessons = model.UserLessons.Select(l => new UserLessonModel(l));
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int ChapterId { get; set; }
        public bool IsPassed { get; set; }
        public IEnumerable<UserLessonModel> UserLessons { get; set; }
    }
}