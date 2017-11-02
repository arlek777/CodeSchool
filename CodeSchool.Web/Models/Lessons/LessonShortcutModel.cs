using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models.Lessons
{
    public class LessonShortcutModel
    {
        public int Id { get; set; }

        [Required]
        public int ChapterId { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string Title { get; set; }

        public int Order { get; set; }
    }
}