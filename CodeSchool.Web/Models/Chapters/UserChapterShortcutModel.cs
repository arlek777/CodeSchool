using System;
using System.Collections.Generic;
using System.Linq;
using CodeSchool.Web.Models.Lessons;

namespace CodeSchool.Web.Models.Chapters
{
    public class UserChapterShortcutModel
    {
        public UserChapterShortcutModel()
        {
            UserLessons = new List<UserLessonShortcutModel>();
        }

        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ChapterId { get; set; }
        public bool IsPassed { get; set; }
        public string ChapterTitle { get; set; }
        public int ChapterOrder { get; set; }

        public UserLessonShortcutModel FirstLesson
        {
            get { return UserLessons.FirstOrDefault();  }
        }
        public List<UserLessonShortcutModel> UserLessons { get; set; }
    }
}