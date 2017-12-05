using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public class Lesson
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ChapterId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string TaskText { get; set; }

        public string AnswerCode { get; set; }

        [Required]
        public string UnitTestsCode { get; set; }

        [Required]
        public string ReporterCode { get; set; }

        [Required]
        public int Order { get; set; }

        public bool Published { get; set; }

        public virtual Chapter Chapter { get; set; }
    }


}
