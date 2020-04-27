using System;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Chapters
{
    public class ShareModal
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(256, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string UserFullName { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(256, ErrorMessage = ValidationResultMessages.MaxLength)]
        [EmailAddress(ErrorMessage = ValidationResultMessages.EmailRequiredOrInvalid)]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int ChapterId { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int LinkLifetimeInDays { get; set; }

        public string TaskDurationTimeLimit { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public Guid CompanyId { get; set; }

        public TimeSpan TaskDurationTimeLimitTimeSpan
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(TaskDurationTimeLimit))
                {
                    return TimeSpan.Parse(TaskDurationTimeLimit);
                }
                return TimeSpan.Zero;
            }
        }
    }

    public class ShareLessonModel : ShareModal
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int LessonId { get; set; }
    }
}