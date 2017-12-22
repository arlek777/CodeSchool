using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public enum LessonType
    {
        Code,
        Test
    }

    public class Lesson
    {
        [Required]
        public int Id { get; set; }

        //public int? CodeLessonId { get; set; }

        [Required]
        public int ChapterId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Text { get; set; }

        public string TaskText { get; set; }

        public string AnswerCode { get; set; }

        public string UnitTestsCode { get; set; }

        public string ReporterCode { get; set; }

        //public LessonType Type { get; set; }

        public int Order { get; set; }

        public bool Published { get; set; }

        public virtual Chapter Chapter { get; set; }

        //public CodeLesson CodeLesson { get; set; }

        //public TestLesson TestLesson { get; set; }
    }
}
