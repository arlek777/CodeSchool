using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Models.Lessons
{

    public class UserLessonResponseModel: UserLessonShortcutResponseModel
    {
        public string Code { get; set; }
        public LessonRequestResponseModel Lesson { get; set; }
    }
}