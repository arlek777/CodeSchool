using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models.Lessons
{
    public class LessonModel: LessonShortcutModel
    {
        [Required]
        [StringLength(5000, MinimumLength = 2)]
        public string Text { get; set; }

        [StringLength(1000)]
        public string StartCode { get; set; }

        [Required]
        [StringLength(5000)]
        public string UnitTestsCode { get; set; }

        [Required]
        [StringLength(5000)]
        public string ReporterCode { get; set; }
    }
}