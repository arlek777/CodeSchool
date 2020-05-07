using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class TaskHeadService : ITaskHeadService
    {
        private readonly IGenericRepository _repository;

        public TaskHeadService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskHead>> GetTaskHeads(Guid companyId, int? categoryId = null)
        {
            List<TaskHead> TaskHeads;
            if (categoryId.HasValue)
            {
                TaskHeads = (await _repository.Where<TaskHead>(c => c.CategoryId == categoryId && c.CompanyId == companyId))
                    .OrderBy(c => c.Order)
                    .ToList();
            }
            else
            {
                TaskHeads = (await _repository.GetAll<TaskHead>()).Where(c => c.CompanyId == companyId)
                    .OrderBy(c => c.Order)
                    .ToList();
            }

            TaskHeads.ForEach(c =>
            {
                c.SubTasks = c.SubTasks.OrderBy(l => l.Order).ToList();
            });

            return TaskHeads;
        }

        public async Task<TaskHead> GetById(Guid companyId, int TaskHeadId)
        {
            var TaskHead = (await GetTaskHeads(companyId)).FirstOrDefault(c => c.Id == TaskHeadId);
            return TaskHead;
        }

        public async Task<TaskHead> AddOrUpdate(TaskHead model)
        {
            var TaskHead = await GetById(model.CompanyId, model.Id);
            if (TaskHead == null)
            {
                model.Order = await GetNextOrder(model.CompanyId);
                _repository.Add(model);
                TaskHead = model;
            }
            else
            {
                TaskHead.Title = model.Title;
                TaskHead.CategoryId = model.CategoryId;
            }
            await _repository.SaveChanges();
            return TaskHead;
        }

        public async Task Remove(Guid companyId, int id)
        {
            var TaskHead = await _repository.Find<TaskHead>(c => c.Id == id && c.CompanyId == companyId);
            _repository.Remove(TaskHead);
            await _repository.SaveChanges();
        }

        public async Task ChangeOrder(Guid companyId, int currentTaskHeadId, int toSwapTaskHeadId)
        {
            var currentTaskHead = await GetById(companyId, currentTaskHeadId);
            var toSwapTaskHead = await GetById(companyId, toSwapTaskHeadId);

            var currentOrder = currentTaskHead.Order;
            currentTaskHead.Order = toSwapTaskHead.Order;
            toSwapTaskHead.Order = currentOrder;

            await _repository.SaveChanges();
        }

        private async Task<int> GetNextOrder(Guid companyId)
        {
            var TaskHeads = await GetTaskHeads(companyId);
            var lastTaskHead = TaskHeads.LastOrDefault();
            return lastTaskHead?.Order + 1 ?? 0;
        }
    }
}
