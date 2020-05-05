using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Models
{
    public class UserTaskSnapshot : UserLesson
    {
        public int UnfocusCount { get; set; }
        public int CopyPasteCount { get; set; }
    }
}