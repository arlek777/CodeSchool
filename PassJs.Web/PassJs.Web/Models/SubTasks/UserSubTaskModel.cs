using CodeSchool.Domain;

namespace PassJs.Web.Models.SubTasks
{

    public class UserSubTaskModel: UserSubTaskShortcutModel
    {
        public int? SelectedAnswerOptionId { get; set; }
        public string Code { get; set; }
        public UserSubTaskAnswerScore? Score { get; set; }
        public SubTaskModel SubTask { get; set; }
        public double TimeLimit { get; set; }
    }
}