using System;
using System.ComponentModel.DataAnnotations;

namespace PassJs.Web.Models.TaskHeads
{
    public class ShareTaskHeadModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(256, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string UserFullName { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        [StringLength(256, ErrorMessage = ValidationResultMessages.MaxLength)]
        [EmailAddress(ErrorMessage = ValidationResultMessages.EmailRequiredOrInvalid)]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int TaskHeadId { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int LinkLifetimeInDays { get; set; }

        public string TaskDurationTimeLimit { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public Guid CompanyId { get; set; }

        public double ParsedTaskDurationTimeLimitTime
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(TaskDurationTimeLimit))
                {
                    return TimeSpan.Parse(TaskDurationTimeLimit).TotalMilliseconds;
                }
                return 0;
            }
        }

        public string CompanyName { get; set; }
    }
}