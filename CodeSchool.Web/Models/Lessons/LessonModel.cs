using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{
    public class LessonModel: LessonShortcutModel
    {
        [StringLength(15000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Text { get; set; }

        [StringLength(5000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string TaskText { get; set; }

        [StringLength(1000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Answer { get; set; }

        [StringLength(15000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string UnitTestsCode { get; set; }

        [StringLength(15000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string ReporterCode { get; set; }

        public ICollection<AnswerLessonOptionModel> AnswerLessonOptions { get; set; }

        public bool PublishNow { get; set; }
    }
}