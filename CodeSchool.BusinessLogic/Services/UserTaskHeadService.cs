using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Extensions;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.BusinessLogic.Models;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class UserTaskHeadService : IUserTaskHeadService
    {
        private readonly IGenericRepository _repository;
        private readonly ITaskHeadService _TaskHeadService;

        public UserTaskHeadService(IGenericRepository repository, ITaskHeadService TaskHeadService)
        {
            _repository = repository;
            _TaskHeadService = TaskHeadService;
        }

        public async Task<ICollection<UserTaskHead>> GetUserTaskHeadsByUserId(Guid userId)
        {
            return await GetUserTaskHeads(new FilterUserTaskHeadModel() { UserId = userId });
        }

        public async Task<ICollection<UserTaskHead>> GetUserTaskHeads(FilterUserTaskHeadModel filterModel)
        {
            var filters = filterModel.GetFilters();
            Expression<Func<UserTaskHead, bool>> resultFilter = filters.FirstOrDefault();
            foreach (var filter in filters.Skip(1))
            {
                resultFilter = resultFilter.And(filter);
            }

            var userTaskHeads = (await _repository.Where(resultFilter))
                .OrderBy(c => c.TaskHead.Order)
                .ToList();

            userTaskHeads.ForEach(c => c.UserSubTasks = c.UserSubTasks.OrderBy(l => l.SubTask.Order).ToList());
            return userTaskHeads;
        }

        public async Task<UserTaskHead> GetUserTaskHeadByTaskHeadId(Guid userId, int TaskHeadId)
        {
            var userTaskHeads = await GetUserTaskHeadsByUserId(userId);
            return userTaskHeads.FirstOrDefault(c => c.TaskHeadId == TaskHeadId);
        }

        public async Task<int> AddTaskHeadSubTasks(Guid userId, Guid companyId, int TaskHeadId,
            double timeLimit)
        {
            var TaskHead = await _TaskHeadService.GetById(companyId, TaskHeadId);

            var newTaskHead = new UserTaskHead()
            {
                TaskHeadId = TaskHead.Id,
                UserId = userId,
                CreatedDt = DateTime.UtcNow,
                TaskDurationLimit = timeLimit
            };
            _repository.Add(newTaskHead);
            await _repository.SaveChanges();

            foreach (var SubTask in TaskHead.SubTasks)
            {
                var newSubTask = new UserSubTask()
                {
                    UserTaskHeadId = newTaskHead.Id,
                    UserId = userId,
                    SubTaskId = SubTask.Id,
                    CreatedDt = DateTime.UtcNow
                };

                _repository.Add(newSubTask);
            }

            await _repository.SaveChanges();

            return newTaskHead.Id;
        }

        public async Task<UserTaskHead> AddOnlyTaskHead(Guid userId, Guid companyId, int TaskHeadId,
            double taskDurationLimit)
        {
            var dbTaskHead = await _TaskHeadService.GetById(companyId, TaskHeadId);
            var newTaskHead = new UserTaskHead()
            {
                TaskHeadId = dbTaskHead.Id,
                UserId = userId,
                CreatedDt = DateTime.UtcNow,
                TaskDurationLimit = taskDurationLimit
            };
            _repository.Add(newTaskHead);
            await _repository.SaveChanges();

            return newTaskHead;
        }

        public async Task<bool> CanOpen(Guid userId, int userTaskHeadId)
        {
            var userTaskHeads = (await GetUserTaskHeadsByUserId(userId)).ToList();
            var user = await _repository.Find<User>(u => u.Id == userId);

            var canOpenTaskHead = new CanOpenTaskHead
            {
                CanOpen = false,
                UserTaskHeadId = userTaskHeadId,
                UserTaskHeads = userTaskHeads,
                User = user
            };

            canOpenTaskHead = canOpenTaskHead
                .CheckOnAlreadyStarted();

            return canOpenTaskHead.CanOpen;
        }

        public async Task<UserTaskHeadSubTaskInfo> GetFirstTaskHeadAndSubTask(Guid userId)
        {
            var userTaskHeads = await GetUserTaskHeads(new FilterUserTaskHeadModel() { UserId = userId });

            if (userTaskHeads == null || !userTaskHeads.Any() || !userTaskHeads.First().UserSubTasks.Any())
            {
                throw new ArgumentNullException($"{nameof(StartUserTask)} User TaskHead was not found.");
            }

            var userTaskHead = userTaskHeads.First();
            var userSubTask = userTaskHead.UserSubTasks.First();

            var result = new UserTaskHeadSubTaskInfo()
            {
                UserTaskHeadId = userTaskHead.Id,
                UserSubTaskId = userSubTask.Id
            };

            return result;
        }

        public async Task<bool> StartUserTask(Guid userId)
        {
            var userTaskHeads = await GetUserTaskHeads(new FilterUserTaskHeadModel() { UserId = userId });

            if (userTaskHeads == null || !userTaskHeads.Any())
            {
                throw new ArgumentNullException($"{nameof(StartUserTask)} User TaskHead was not found.");
            }

            var userTaskHead = userTaskHeads.First();
            if (userTaskHead.StartedDt.HasValue)
            {
                throw new InvalidDataException($"{nameof(StartUserTask)} This user TaskHead has already started and cannot be started again.");
            }

            userTaskHead.StartedDt = DateTime.UtcNow;
            await _repository.SaveChanges();

            return true;
        }

        public async Task FinishUserTask(Guid userId)
        {
            var userTaskHeads = await GetUserTaskHeads(new FilterUserTaskHeadModel() { UserId = userId });

            if (userTaskHeads == null || !userTaskHeads.Any())
            {
                throw new ArgumentNullException($"{nameof(StartUserTask)} User TaskHead was not found.");
            }

            var userTaskHead = userTaskHeads.First();
            if (!userTaskHead.StartedDt.HasValue)
            {
                throw new InvalidDataException($"{nameof(StartUserTask)} This user TaskHead has not been started and cannot be finished.");
            }

            userTaskHead.FinishedDt = DateTime.UtcNow;
            await _repository.SaveChanges();
        }
    }
}