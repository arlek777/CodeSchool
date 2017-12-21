using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain.Tests
{
    public class TestQuestionOption
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TestQuestionId { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}