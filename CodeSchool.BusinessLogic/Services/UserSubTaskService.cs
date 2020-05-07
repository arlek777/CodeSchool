using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Extensions;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.BusinessLogic.Models;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class UserSubTaskService : IUserSubTaskService
    {
        private readonly IGenericRepository _repository;
        private readonly IUserTaskHeadService _userTaskHeadService;

        public UserSubTaskService(IGenericRepository repository, IUserTaskHeadService userTaskHeadService)
        {
            _repository = repository;
            _userTaskHeadService = userTaskHeadService;
        }

        public async Task<ICollection<UserSubTask>> GetUserSubTasksById(Guid userId, int userTaskHeadId)
        {
            return (await _repository.Where<UserSubTask>(c => c.UserId == userId && c.UserTaskHeadId == userTaskHeadId))
                .OrderBy(l => l.SubTask.Order)
                .ToList();
        }

        public async Task<UserSubTask> GetUserSubTaskById(Guid userId, int userSubTaskId)
        {
            return await _repository.Find<UserSubTask>(c => c.Id == userSubTaskId && c.UserId == userId);
        }

        public async Task Add(Guid userId, int SubTaskId, int TaskHeadId, double taskDurationLimit)
        {
            var user = await _repository.Find<User>(u => u.Id == userId);
            var userTaskHead = await _userTaskHeadService.GetUserTaskHeadByTaskHeadId(user.Id, TaskHeadId);
            if (userTaskHead == null)
            {
                userTaskHead = await _userTaskHeadService.AddOnlyTaskHead(userId, user.CompanyId, TaskHeadId, taskDurationLimit);
            }

            _repository.Add(new UserSubTask()
            {
                UserId = user.Id,
                UserTaskHeadId = userTaskHead.Id,
                SubTaskId = SubTaskId,
                CreatedDt = DateTime.UtcNow
            });
            await _repository.SaveChanges();
        }

        public async Task<UserSubTask> Update(UserSubTask model)
        {
            var userSubTask = await GetUserSubTaskById(model.UserId, model.Id);
            userSubTask.IsPassed = model.IsPassed;
            userSubTask.UpdatedDt = DateTime.UtcNow;

            switch (userSubTask.SubTask.Type)
            {
                case SubTaskType.Code:
                    userSubTask.Code = model.Code;
                    break;
                case SubTaskType.LongAnswer:
                    userSubTask.Score = model.Score;
                    break;
                case SubTaskType.Test:
                    userSubTask.SelectedAnswerOptionId = model.SelectedAnswerOptionId;
                    break;
            }


            userSubTask.UserTaskHead.IsPassed = (await GetUserSubTasksById(model.UserId, model.UserTaskHeadId))
                .All(l => l.IsPassed);

            await _repository.SaveChanges();

            return userSubTask;
        }

        public async Task<UserSubTask> SaveSnapshot(UserTaskSnapshot model)
        {
            var now = DateTime.UtcNow;

            var userSubTask = await GetUserSubTaskById(model.UserId, model.Id);
            userSubTask.UserTaskHead.CopyPasteCount += model.CopyPasteCount;
            userSubTask.UserTaskHead.UnfocusCount += model.UnfocusCount;
            userSubTask.UpdatedDt = now;
            userSubTask.UserTaskHead.UpdatedDt = now;

            userSubTask.CodeSnapshots.Add(new CodeSnapshot()
            {
                Code = model.Code,
                CreatedDt = now
            });

            await _repository.SaveChanges();

            return userSubTask;
        }

        public async Task<bool> CanOpen(Guid userId, int userTaskHeadId, int userSubTaskId)
        {
            var userSubTasks = await GetUserSubTasksById(userId, userTaskHeadId);
            var user = await _repository.Find<User>(u => u.Id == userId);
         
            var canOpenSubTask = new CanOpenSubTask
            {
                CanOpen = false,
                UserSubTaskId = userSubTaskId,
                UserSubTasks = userSubTasks,
                User = user
            };

            canOpenSubTask = canOpenSubTask
                .CheckType()
                .CheckFirst()
                .CheckOnPassed()
                .CheckAllPreviousPassed();

            return true;
        }
    }
}