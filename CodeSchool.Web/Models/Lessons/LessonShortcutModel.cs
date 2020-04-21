using System;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Domain;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{
    public class LessonShortcutModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int ChapterId { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(256, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Title { get; set; }

        public int Order { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public LessonType Type { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public LessonLevel Level { get; set; }

        public Guid CompanyId { get; set; }

        public bool Published { get; set; }
    }
}