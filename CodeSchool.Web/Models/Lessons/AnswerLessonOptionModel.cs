using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{
    public class AnswerLessonOptionModel
    {
        public int Id { get; set; }
        public int LessonId { get; set; }

        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}