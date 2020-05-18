using PassJs.DomainModels;

namespace PassJs.Core.Models
{
    public class UserTaskSnapshot : UserSubTask
    {
        public int UnfocusCount { get; set; }
        public int CopyPasteCount { get; set; }
    }
}