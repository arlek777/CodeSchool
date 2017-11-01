using System.ComponentModel.DataAnnotations;
using CodeSchool.Domain;

namespace CodeSchool.Web.Models
{
    public class LessonModel
    {
        public LessonModel()
        {
        }

        public LessonModel(Lesson lesson)
        {
            Id = lesson.Id;
            ChapterId = lesson.ChapterId;
            Title = lesson.Title;
            Text = lesson.Text;
            StartCode = lesson.StartCode;
            UnitTestsCode = lesson.UnitTestsCode;
            ReporterCode = lesson.ReporterCode;
            Order = lesson.Order;
        }

        public int Id { get; set; }

        [Required]
        public int ChapterId { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string Title { get; set; }

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

        public int Order { get; set; }
    }
}