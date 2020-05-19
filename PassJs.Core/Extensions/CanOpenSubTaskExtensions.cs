using System.Linq;
using PassJs.Core.Models;
using PassJs.DomainModels;

namespace PassJs.Core.Extensions
{
    internal static class CanOpenSubTaskExtensions
    {
        public static CanOpenSubTask CheckType(this CanOpenSubTask model)
        {
            var userSubTask = model.UserSubTasks.FirstOrDefault(c => c.Id == model.UserSubTaskId);
            model.CanOpen = userSubTask != null && userSubTask.SubTask.Type != SubTaskType.Code;

            return model;
        }

        public static CanOpenSubTask CheckUserAdmin(this CanOpenSubTask model)
        {
            if (model.CanOpen) return model;

            model.CanOpen = model.User.IsAdmin;
            return model;
        }

        public static CanOpenSubTask CheckFirst(this CanOpenSubTask model)
        {
            if (model.CanOpen) return model;

            var firstSubTask = model.UserSubTasks.FirstOrDefault();
            model.CanOpen = firstSubTask != null && firstSubTask.Id == model.UserSubTaskId;

            return model;
        }

        public static CanOpenSubTask CheckOnPassed(this CanOpenSubTask model)
        {
            if (model.CanOpen) return model;

            var userSubTask = model.UserSubTasks.FirstOrDefault(l => l.Id == model.UserSubTaskId && l.IsPassed);
            model.CanOpen = userSubTask != null;

            return model;
        }

        public static CanOpenSubTask CheckAllPreviousPassed(this CanOpenSubTask model)
        {
            if (model.CanOpen) return model;

            var previousSubTasks = model.UserSubTasks
                .Where(u => u.SubTask.Type == SubTaskType.Code)
                .TakeWhile(l => l.Id != model.UserSubTaskId).ToList();
            var allPreviousPassed = previousSubTasks.Any() && previousSubTasks.All(l => l.IsPassed);
            model.CanOpen = allPreviousPassed;

            return model;
        }
    }
}