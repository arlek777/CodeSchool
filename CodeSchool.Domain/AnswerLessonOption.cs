using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public class AnswerLessonOption
    {
        [Required]
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}