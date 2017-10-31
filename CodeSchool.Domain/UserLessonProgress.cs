using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSchool.Domain
{
    public class UserLessonProgress
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int LessonId { get; set; }
        public Guid UserChapterProgressId { get; set; }
        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }
        public string Code { get; set; }
        public DateTime UpdatedDt { get; set; }
    }
}