using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public enum UserSubTaskAnswerScore
    {
        DontKnow = 0,
        HardToRemember,
        KnowIt
    }

    public class UserSubTask
    {
        public UserSubTask()
        {
            CodeSnapshots = new List<CodeSnapshot>();
        }

        [Required]
        public int Id { get; set; }

        public int SubTaskId { get; set; }

        public int UserTaskHeadId { get; set; }

        public Guid UserId { get; set; }

        public bool IsPassed { get; set; }

        public string Code { get; set; }

        public DateTime? CreatedDt { get; set; }
        public DateTime? UpdatedDt { get; set; }

        // Maybe will be removed in future
        public int? SelectedAnswerOptionId { get; set; }

        public UserSubTaskAnswerScore? Score { get; set; }
        
        // Virtual properties
        public virtual User User { get; set; }
        public virtual UserTaskHead UserTaskHead { get; set; }
        public virtual SubTask SubTask { get; set; }
        public virtual ICollection<CodeSnapshot> CodeSnapshots { get; set; }
    }
}