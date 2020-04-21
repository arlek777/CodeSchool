using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public enum ChapterType
    {
        Code = 0,
        Test
    }

    public class Chapter
    {
        public Chapter()
        {
            Lessons = new List<Lesson>();
            UserChapters = new List<UserChapter>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        public int Order { get; set; }

        public ChapterType Type { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<UserChapter> UserChapters { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}