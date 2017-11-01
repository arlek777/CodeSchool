using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Order = chapter.Order;

            Lessons = chapter.Lessons.Select(l => new LessonModel(l));
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string Title { get; set; }

        public int Order { get; set; }

        public IEnumerable<LessonModel> Lessons { get; set; }
    }
}