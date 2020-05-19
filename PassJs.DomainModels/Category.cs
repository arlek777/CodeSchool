using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PassJs.DomainModels
{
    public class Category: ISimpleEntity
    {
        public Category()
        {
            TaskHeads = new List<TaskHead>();
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public virtual ICollection<TaskHead> TaskHeads { get; set; }
    }
}