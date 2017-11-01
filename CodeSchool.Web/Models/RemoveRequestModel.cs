using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models
{
    public class RemoveRequestModel
    {
        [Required]
        public int Id { get; set; }
    }
}