using System.ComponentModel.DataAnnotations;
using CodeSchool.Domain;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{
    public class AnswerLessonOptionModel
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class LessonShortcutModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int ChapterId { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(256, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Title { get; set; }

        public int Order { get; set; }

        public LessonType Type { get; set; }

        public LessonLevel Level { get; set; }

        public bool Published { get; set; }
    }
}