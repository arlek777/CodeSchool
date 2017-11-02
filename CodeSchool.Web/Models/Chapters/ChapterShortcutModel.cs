using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Models.Lessons;

namespace CodeSchool.Web.Models.Chapters
{
    public class ChapterShortcutModel
    {
        public ChapterShortcutModel()
        {
            Lessons = new List<LessonShortcutModel>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string Title { get; set; }

        public int Order { get; set; }

        public IEnumerable<LessonShortcutModel> Lessons { get; set; }
    }
}