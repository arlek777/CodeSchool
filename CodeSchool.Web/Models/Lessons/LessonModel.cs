using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Domain;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{
    public class LessonModel: LessonShortcutModel
    {
        [RequiredForLessonType(LessonType.Code, ErrorMessage = ValidationResultMessages.RequiredField)]
        public string Text { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public string TaskText { get; set; }

        [RequiredForLessonType(LessonType.Code, LessonType.LongAnswer, ErrorMessage = ValidationResultMessages.RequiredField)]
        public string Answer { get; set; }

        [RequiredForLessonType(LessonType.Code, ErrorMessage = ValidationResultMessages.RequiredField)]
        public string UnitTestsCode { get; set; }

        [RequiredForLessonType(LessonType.Code, ErrorMessage = ValidationResultMessages.RequiredField)]
        public string ReporterCode { get; set; }

        [RequiredForLessonType(LessonType.Test, ErrorMessage = ValidationResultMessages.RequiredField)]
        public AnswerLessonOptionModel[] AnswerLessonOptions { get; set; }

        public bool PublishNow { get; set; }
    }
}