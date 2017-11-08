using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models.Lessons
{
    public class UserLessonUpdateModel
    {
        public int Id { get; set; }
        public int UserChapterId { get; set; }
        public Guid UserId { get; set; }
        public bool IsPassed { get; set; }

        [Required]
        [StringLength(5000)]
        public string Code { get; set; }
    }
}