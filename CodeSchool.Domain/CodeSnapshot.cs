using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Domain
{
    public class CodeSnapshot: ISimpleEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserSubTaskId { get; set; }

        [Required]
        public DateTime CreatedDt { get; set; }

        public string Code { get; set; }
    }
}