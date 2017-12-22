using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public class Category: ISimpleEntity
    {
        public Category()
        {
            Chapters = new List<Chapter>();
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }
    }
}