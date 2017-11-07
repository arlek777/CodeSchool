using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public class Lesson
    {
        [Required]
        public int Id { get; set; }
        public int ChapterId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public string StartCode { get; set; }
        [Required]
        public string UnitTestsCode { get; set; }
        [Required]
        public string ReporterCode { get; set; }

        public int Order { get; set; }
        public bool IsRemoved { get; set; }

        public virtual Chapter Chapter { get; set; }
    }


}
