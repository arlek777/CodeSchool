using System.Linq;
using CodeSchool.BusinessLogic.Models;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Extensions
{
    internal static class CanOpenTaskHeadExtensions
    {
        public static CanOpenTaskHead CheckType(this CanOpenTaskHead model)
        {
            if (model.CanOpen) return model;

            var userTaskHead = model.UserTaskHeads.FirstOrDefault(c => c.Id == model.UserTaskHeadId);
            model.CanOpen = userTaskHead != null && userTaskHead.TaskHead.Type != TaskType.Code;

            return model;
        }

        public static CanOpenTaskHead CheckUserAdmin(this CanOpenTaskHead model)
        {
            if (model.CanOpen) return model;

            model.CanOpen = model.User.IsAdmin;
            return model;
        }

        public static CanOpenTaskHead CheckFirst(this CanOpenTaskHead model)
        {
            if (model.CanOpen) return model;

            var firstTaskHead = model.UserTaskHeads.FirstOrDefault();
            var isFirst = firstTaskHead != null && firstTaskHead.Id == model.UserTaskHeadId;
            model.CanOpen = isFirst;
            return model;
        }

        public static CanOpenTaskHead CheckOnPassed(this CanOpenTaskHead model)
        {
            if (model.CanOpen) return model;

            var userTaskHead = model.UserTaskHeads.FirstOrDefault(c => c.Id == model.UserTaskHeadId);
            model.CanOpen = userTaskHead != null && userTaskHead.IsPassed;

            return model;
        }

        public static CanOpenTaskHead CheckOnAlreadyStarted(this CanOpenTaskHead model)
        {
            if (model.CanOpen) return model;

            var userTaskHead = model.UserTaskHeads.FirstOrDefault(c => c.Id == model.UserTaskHeadId);
            model.CanOpen = userTaskHead != null && !userTaskHead.StartedDt.HasValue;

            return model;
        }

        public static CanOpenTaskHead CheckAllPreviousPassed(this CanOpenTaskHead model)
        {
            if (model.CanOpen) return model;

            var allPreviousPassed = model.UserTaskHeads
                .Where(u => u.TaskHead.Type == TaskType.Code)
                .TakeWhile(c => c.Id != model.UserTaskHeadId)
                .All(c => c.IsPassed);
            model.CanOpen = allPreviousPassed;
            return model;
        }
    }
}