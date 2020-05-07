using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Models;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IUserSubTaskService
    {
        Task<ICollection<UserSubTask>> GetUserSubTasksById(Guid userId, int userTaskHeadId);
        Task<UserSubTask> GetUserSubTaskById(Guid userId, int userSubTaskId);
        Task Add(Guid userId, int SubTaskId, int TaskHeadId, double taskDurationLimit);
        Task<UserSubTask> Update(UserSubTask model);
        Task<UserSubTask> SaveSnapshot(UserTaskSnapshot model);
        Task<bool> CanOpen(Guid userId, int userTaskHeadId, int userSubTaskId);
    }
}