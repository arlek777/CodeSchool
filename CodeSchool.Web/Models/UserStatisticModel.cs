using System.Collections.Generic;

namespace CodeSchool.Web.Models
{
    public class UserStatisticModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<string> PassedLessons { get; set; }
    }
}