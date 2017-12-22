namespace CodeSchool.Domain
{
    public class TestLessonOption
    {
        public int Id { get; set; }
        public int TestLessonId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public TestLesson TestLesson { get; set; }
    }
}