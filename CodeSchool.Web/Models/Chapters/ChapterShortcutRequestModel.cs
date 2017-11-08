using System.Collections.Generic;
using CodeSchool.Web.Models.Lessons;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Chapters
{
    public class ChapterShortcutRequestModel
    {
        public ChapterShortcutRequestModel()
        {
            Lessons = new List<LessonShortcutRequestModel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(256, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Title { get; set; }

        public int Order { get; set; }

        public IEnumerable<LessonShortcutRequestModel> Lessons { get; set; }
    }
}