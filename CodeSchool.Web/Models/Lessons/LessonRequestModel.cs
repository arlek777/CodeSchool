using System;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{
    public class LessonRequestModel: LessonShortcutRequestModel
    {
        [Required(ErrorMessage =ValidationResultMessages.RequiredField)]
        [StringLength(5000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Text { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(1000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string StartCode { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(5000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string UnitTestsCode { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(5000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string ReporterCode { get; set; }
    }
}