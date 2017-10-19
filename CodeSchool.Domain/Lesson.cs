namespace CodeSchool.Domain
{
    public class Lesson
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string StartCode { get; set; }
        public string UnitTestFilePath { get; set; }
        public string ReporterFilePath { get; set; }

        public virtual Chapter Chapter { get; set; }
    }


}
