using System.ComponentModel.DataAnnotations;

namespace PassJs.DomainModels
{
    public class AnswerSubTaskOption
    {
        [Required]
        public int Id { get; set; }
        public int SubTaskId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public virtual SubTask SubTask { get; set; }
    }
}