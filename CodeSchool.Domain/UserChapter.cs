using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSchool.Domain
{
    public class UserChapter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int ChapterId { get; set; }
        public bool IsPassed { get; set; }

        public virtual User User { get; set; }
        public virtual Chapter Chapter { get; set; }
        public virtual ICollection<UserLesson> UserLessons { get; set; }
    }
}