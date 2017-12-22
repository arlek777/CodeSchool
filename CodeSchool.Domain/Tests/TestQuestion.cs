using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain.Tests
{
    public class TestQuestion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TestThemeId { get; set; }
        public string Text { get; set; }
        public string AnswerText { get; set; }
        public bool IsPublished { get; set; }

        public int Order { get; set; }

        public virtual ICollection<TestQuestionOption> TestQuestionOptions { get; set; }
    }
}