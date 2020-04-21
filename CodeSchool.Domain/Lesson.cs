using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public enum LessonType
    {
        Code = 0,
        Test,
        LongAnswer
    }

    public enum LessonLevel
    {
        Junior = 0,
        Middle,
        Senior
    }

    public class Lesson
    {
        public Lesson()
        {
            AnswerLessonOptions = new List<AnswerLessonOption>();
            UserLessons = new List<UserLesson>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int ChapterId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        public string Text { get; set; }

        public string Answer { get; set; }

        public string TaskText { get; set; }

        public string UnitTestsCode { get; set; }

        public string ReporterCode { get; set; }

        public LessonType Type { get; set; }

        public LessonLevel Level { get; set; }

        public int Order { get; set; }

        public bool Published { get; set; }

        public virtual Chapter Chapter { get; set; }

        public virtual ICollection<UserLesson> UserLessons { get; set; }

        public virtual ICollection<AnswerLessonOption> AnswerLessonOptions { get; set; }
    }
}
