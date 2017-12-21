using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain.Tests
{
    public class UserTestQuestion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TestQuestionId { get; set; }
        public int SelectedTestOptionId { get; set; }
        public int Rate { get; set; }
        public bool IsCorrect { get; set; }

        public virtual TestQuestion TestQuestion { get; set; }
    }
}