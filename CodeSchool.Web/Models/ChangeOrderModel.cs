using System.ComponentModel.DataAnnotations;

namespace CodeSchool.Web.Models
{
    public class ChangeOrderModel
    {
        [Required]
        public int CurrentId { get; set; }
        [Required]
        public int ToSwapId { get; set; }
    }
}