using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public class CodeSnapshot: ISimpleEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserLessonId { get; set; }

        [Required]
        public DateTime CreatedDt { get; set; }

        [Required]
        public string Code { get; set; }
    }
}