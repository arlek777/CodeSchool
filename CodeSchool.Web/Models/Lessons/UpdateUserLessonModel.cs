using System;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Domain;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{
    public class UpdateUserLessonModel
    {
        public int Id { get; set; }
        public int UserChapterId { get; set; }
        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }

        [StringLength(5000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Code { get; set; }
        public int? SelectedAnswerOptionId { get; set; }
        public UserLessonAnswerScore? Score { get; set; }
    }
}