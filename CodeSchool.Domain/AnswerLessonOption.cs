namespace CodeSchool.Domain
{
    public class AnswerLessonOption
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}