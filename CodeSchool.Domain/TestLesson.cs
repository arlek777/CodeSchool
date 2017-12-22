using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSchool.Domain
{
    public class TestLesson
    {
        public TestLesson()
        {
            TestLessonOptions = new List<TestLessonOption>();
        }

        [ForeignKey("")]
        public int Id { get; set; }

        public string Answer { get; set; }

        public ICollection<TestLessonOption> TestLessonOptions { get; set; }
    }
}