using System.Collections.Generic;

namespace CodeSchool.Domain
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual IEnumerable<Lesson> Lessons { get; set; }
    }
}