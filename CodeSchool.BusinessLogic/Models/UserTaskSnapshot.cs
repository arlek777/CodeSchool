using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Models
{
    public class UserTaskSnapshot : UserSubTask
    {
        public int UnfocusCount { get; set; }
        public int CopyPasteCount { get; set; }
    }
}