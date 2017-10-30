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
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string StartCode { get; set; }
        public string UnitTestsCode { get; set; }
        public string ReporterCode { get; set; }
        public int Order { get; set; }
    }
}