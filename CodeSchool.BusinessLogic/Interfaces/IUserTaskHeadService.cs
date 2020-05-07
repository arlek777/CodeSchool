using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Models;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IUserTaskHeadService
    {
        Task<ICollection<UserTaskHead>> GetUserTaskHeadsByUserId(Guid userId);
        Task<ICollection<UserTaskHead>> GetUserTaskHeads(FilterUserTaskHeadModel filterModel);
        Task<UserTaskHead> GetUserTaskHeadByTaskHeadId(Guid userId, int TaskHeadId);
        Task<int> AddTaskHeadSubTasks(Guid userId, Guid companyId, int TaskHeadId, double timeLimit);
        Task<UserTaskHead> AddOnlyTaskHead(Guid userId, Guid companyId, int TaskHeadId, double taskDurationLimit);
        Task<bool> CanOpen(Guid userId, int userTaskHeadId);

        Task<UserTaskHeadSubTaskInfo> GetFirstTaskHeadAndSubTask(Guid userId);
        Task<bool> StartUserTask(Guid userId);
        Task FinishUserTask(Guid userId);
    }
}