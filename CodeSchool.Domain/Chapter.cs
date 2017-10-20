using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public class Chapter
    {
        public Chapter()
        {
            Lessons = new List<Lesson>();
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}