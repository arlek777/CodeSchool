using System;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{
    public class LessonRequestResponseModel: LessonShortcutRequestResponseModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(15000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Text { get; set; }

        [StringLength(5000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string TaskText { get; set; }

        [StringLength(1000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string AnswerCode { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(15000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string UnitTestsCode { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(15000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string ReporterCode { get; set; }
    }
}