using System;
using System.Collections.Generic;
using CodeSchool.Web.Models.Lessons;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Domain;

namespace CodeSchool.Web.Models.Chapters
{
    public class ChapterShortcutModel
    {
        public ChapterShortcutModel()
        {
            Lessons = new List<LessonShortcutModel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(256, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int CategoryId { get; set; }

        public Guid CompanyId { get; set; }

        public int Order { get; set; }

        public ChapterType Type { get; set; }

        public IEnumerable<LessonShortcutModel> Lessons { get; set; }
    }
}