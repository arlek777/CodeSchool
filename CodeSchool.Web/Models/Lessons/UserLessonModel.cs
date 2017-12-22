using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{

    public class UserLessonModel: UserLessonShortcutModel
    {
        public int SelectedAnswerOptionId { get; set; }
        public string Code { get; set; }
        public LessonModel Lesson { get; set; }
    }
}