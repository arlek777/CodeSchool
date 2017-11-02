using System;
using System.Collections.Generic;
using CodeSchool.Web.Models.Lessons;

namespace CodeSchool.Web.Models.Chapters
{
    public class UserChapterShortcutModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int ChapterId { get; set; }
        public bool IsPassed { get; set; }
        public string ChapterTitle { get; set; }
        public int ChapterOrder { get; set; }
        public LinkedList<UserLessonShortcutModel> UserLessons { get; set; }
    }
}