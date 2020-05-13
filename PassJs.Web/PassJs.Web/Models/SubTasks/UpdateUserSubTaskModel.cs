using System;
using System.ComponentModel.DataAnnotations;
using CodeSchool.Domain;

namespace PassJs.Web.Models.SubTasks
{
    public class UpdateUserSubTaskModel
    {
        public int Id { get; set; }
        public int UserTaskHeadId { get; set; }
        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }

        [StringLength(5000, ErrorMessage = ValidationResultMessages.MaxLength)]
        public string Code { get; set; }
        public int? SelectedAnswerOptionId { get; set; }
        public UserSubTaskAnswerScore? Score { get; set; }
    }
}