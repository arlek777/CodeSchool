using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSchool.Domain
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<UserChapter> UserChapters { get; set; }
        public virtual ICollection<UserLesson> UserLessons { get; set; }
    }
}