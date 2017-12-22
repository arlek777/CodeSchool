using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSchool.Domain
{
    public class CodeLesson
    {
        [ForeignKey("")]
        public int Id { get; set; }

        public string AnswerCode { get; set; }

        public string UnitTestsCode { get; set; }

        public string ReporterCode { get; set; }
    }
}