using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain.Tests
{
    public class TestTheme: ISimpleEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TestCategoryId { get; set; }
        [Required]
        public string Title { get; set; }

        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
    }
}