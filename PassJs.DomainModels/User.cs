using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassJs.DomainModels
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }
        public virtual ICollection<UserTaskHead> UserTaskHeads { get; set; }
        public virtual ICollection<UserSubTask> UserSubTasks { get; set; }
    }
}