using System.Collections.Generic;
using System.Linq;
using CodeSchool.Domain;

namespace CodeSchool.Web.Models
{
    public class ChapterModel
    {
        public ChapterModel()
        {
        }
        public ChapterModel(Chapter chapter)
        {
            Id = chapter.Id;
            Title = chapter.Title;

            Lessons = chapter.Lessons.Select(l => new LessonModel(l));
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<LessonModel> Lessons { get; set; }
    }
}