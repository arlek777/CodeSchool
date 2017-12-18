using System.Linq;
using CodeSchool.BusinessLogic.Models;

namespace CodeSchool.BusinessLogic.Extensions
{
    internal static class CanOpenLessonExtensions
    {
        public static CanOpenLesson CheckUserAdmin(this CanOpenLesson model)
        {
            if (model.CanOpen) return model;

            model.CanOpen = model.User.IsAdmin;
            return model;
        }

        public static CanOpenLesson CheckFirst(this CanOpenLesson model)
        {
            if (model.CanOpen) return model;

            var firstLesson = model.UserLessons.FirstOrDefault();
            model.CanOpen = firstLesson != null && firstLesson.Id == model.UserLessonId;

            return model;
        }

        public static CanOpenLesson CheckOnPassed(this CanOpenLesson model)
        {
            if (model.CanOpen) return model;

            var userLesson = model.UserLessons.FirstOrDefault(l => l.Id == model.UserLessonId && l.IsPassed);
            model.CanOpen = userLesson != null;

            return model;
        }

        public static CanOpenLesson CheckAllPreviousPassed(this CanOpenLesson model)
        {
            if (model.CanOpen) return model;

            var previousLessons = model.UserLessons.TakeWhile(l => l.Id != model.UserLessonId).ToList();
            var allPreviousPassed = previousLessons.Any() && previousLessons.All(l => l.IsPassed);
            model.CanOpen = allPreviousPassed;

            return model;
        }
    }
}