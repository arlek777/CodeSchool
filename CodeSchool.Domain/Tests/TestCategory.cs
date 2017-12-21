using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain.Tests
{
    public class TestCategory: ISimpleEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public virtual ICollection<TestTheme> TestThemes { get; set; }
    }
}
