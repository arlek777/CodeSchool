using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Domain;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{
    public class LessonModel: LessonShortcutModel
    {
        [RequiredForLessonType(LessonType.Code, ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(15000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Text { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(5000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string TaskText { get; set; }

        [RequiredForLessonType(LessonType.Code, LessonType.LongAnswer, ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(1000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Answer { get; set; }

        [RequiredForLessonType(LessonType.Code, ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(15000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string UnitTestsCode { get; set; }

        [RequiredForLessonType(LessonType.Code, ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(15000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string ReporterCode { get; set; }

        [RequiredForLessonType(LessonType.Test, ErrorMessage = ValidationResultMessages.RequiredField)]
        public AnswerLessonOptionModel[] AnswerLessonOptions { get; set; }

        public bool PublishNow { get; set; }
    }
}