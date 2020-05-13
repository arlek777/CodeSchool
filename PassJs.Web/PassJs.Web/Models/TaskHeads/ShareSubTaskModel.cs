using System.ComponentModel.DataAnnotations;

namespace PassJs.Web.Models.TaskHeads
{
    public class ShareSubTaskModel : ShareTaskHeadModel
    {
        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public int SubTaskId { get; set; }
    }
}