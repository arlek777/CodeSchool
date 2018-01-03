using System.Linq;
using CodeSchool.BusinessLogic.Models;

namespace CodeSchool.BusinessLogic.Extensions
{
    internal static class CanOpenChapterExtensions
    {
        public static CanOpenChapter CheckUserAdmin(this CanOpenChapter model)
        {
            if (model.CanOpen) return model;

            model.CanOpen = model.User.IsAdmin;
            return model;
        }

        public static CanOpenChapter CheckFirst(this CanOpenChapter model)
        {
            if (model.CanOpen) return model;

            var firstChapter = model.UserChapters.FirstOrDefault();
            var isFirst = firstChapter != null && firstChapter.Id == model.UserChapterId;
            model.CanOpen = isFirst;
            return model;
        }

        public static CanOpenChapter CheckOnPassed(this CanOpenChapter model)
        {
            if (model.CanOpen) return model;

            var userChapter = model.UserChapters.FirstOrDefault(c => c.Id == model.UserChapterId);
            model.CanOpen = userChapter != null && userChapter.IsPassed;

            return model;
        }

        public static CanOpenChapter CheckAllPreviousPassed(this CanOpenChapter model)
        {
            if (model.CanOpen) return model;

            var allPreviousPassed = model.UserChapters.TakeWhile(c => c.Id != model.UserChapterId).All(c => c.IsPassed);
            model.CanOpen = allPreviousPassed;
            return model;
        }
    }
}