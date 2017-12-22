using System;

namespace CodeSchool.Web.Models.Chapters
{
    public class CanOpenChapterModel
    {
        public Guid UserId { get; set; }
        public int UserChapterId { get; set; }
    }
}