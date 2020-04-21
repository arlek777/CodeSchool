using System;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{
    public class PublishLessonModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int LessonId { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int ChapterId { get; set; }
    }
}
