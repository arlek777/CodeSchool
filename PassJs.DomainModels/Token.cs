using System;
using System.ComponentModel.DataAnnotations;

namespace PassJs.DomainModels
{
    public class Token: ISimpleEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid TokenValue { get; set; }

        public string ExtraData { get; set; }

        [Required]
        public DateTime CreatedDt { get; set; }

        [Required]
        public int LifetimeInDays { get; set; }

        public virtual User User { get; set; }
    }
}