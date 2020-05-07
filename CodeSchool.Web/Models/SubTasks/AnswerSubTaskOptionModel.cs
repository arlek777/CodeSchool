using System.ComponentModel.DataAnnotations;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.SubTasks
{
    public class AnswerSubTaskOptionModel
    {
        public int Id { get; set; }
        public int SubTaskId { get; set; }

        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}