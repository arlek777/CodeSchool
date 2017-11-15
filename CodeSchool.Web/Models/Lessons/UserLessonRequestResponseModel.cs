using System;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{
    public class UserLessonRequestResponseModel
    {
        public int Id { get; set; }
        public int UserChapterId { get; set; }
        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(5000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Code { get; set; }
    }
}