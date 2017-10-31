using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSchool.Domain
{
    public class UserChapterProgress
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int ChapterId { get; set; }
        public bool IsPassed { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<UserLessonProgress> UserLessonProgresses { get; set; }
    }
}