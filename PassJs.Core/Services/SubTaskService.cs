using System;
using System.Linq;
using System.Threading.Tasks;
using PassJs.Core.Interfaces;
using PassJs.DataAccess;
using PassJs.DomainModels;

namespace PassJs.Core.Services
{
    public class SubTaskService : ISubTaskService
    {
        private readonly IGenericRepository _repository;
        private readonly IAnswerSubTaskOptionService _answerSubTaskOptionService;
        private readonly ITaskHeadService _TaskHeadService;

        public SubTaskService(IGenericRepository repository, IAnswerSubTaskOptionService answerSubTaskOptionService,
            ITaskHeadService TaskHeadService)
        {
            _repository = repository;
            _answerSubTaskOptionService = answerSubTaskOptionService;
            _TaskHeadService = TaskHeadService;
        }

        public async Task<SubTask> GetById(Guid companyId, int id)
        {
            return await _repository.Find<SubTask>(l => l.Id == id && l.CompanyId == companyId);
        }

        public async Task<SubTask> AddOrUpdate(SubTask model)
        {
            var dbSubTask = await GetById(model.CompanyId, model.Id);
            if (dbSubTask == null)
            {
                var TaskHead = await _repository.Find<TaskHead>(c => c.Id == model.TaskHeadId && c.CompanyId == model.CompanyId);
                if (!TaskHead.SubTasks.Any())
                {
                    TaskHead.Type = model.Type == SubTaskType.Code ? TaskType.Code : TaskType.Test;
                }

                dbSubTask = new SubTask()
                {
                    CompanyId = model.CompanyId,
                    Type = model.Type,
                    TaskHeadId = model.TaskHeadId,
                    Published = model.Published
                };

                await FillDbSubTaskForType(model, dbSubTask);
                dbSubTask.Order = await GetNextOrder(model.CompanyId, model.TaskHeadId);
                _repository.Add(dbSubTask);
            }
            else
            {
                await FillDbSubTaskForType(model, dbSubTask);
            }
           
            await _repository.SaveChanges();
            return dbSubTask;
        }

        public async Task ChangeOrder(Guid companyId, int currentSubTaskId, int toSwapSubTaskId)
        {
            var currentSubTask = await GetById(companyId, currentSubTaskId);
            var toSwapSubTask = await GetById(companyId, toSwapSubTaskId);

            var toSwapOrder = toSwapSubTask.Order;
            toSwapSubTask.Order = currentSubTask.Order;
            currentSubTask.Order = toSwapOrder;

            await _repository.SaveChanges();
        }

        public async Task Remove(Guid companyId, int id)
        {
            var SubTask = await GetById(companyId, id);
            _repository.Remove(SubTask);
            await _repository.SaveChanges();
        }

        private async Task FillDbSubTaskForType(SubTask model, SubTask dbSubTask)
        {
            dbSubTask.Answer = model.Answer;
            dbSubTask.TaskText = model.TaskText;
            dbSubTask.Text = model.Text;
            dbSubTask.Title = model.Title;
            dbSubTask.Level = model.Level;
            dbSubTask.CompanyId = model.CompanyId;

            if (model.Type == SubTaskType.Test)
            {
                await _answerSubTaskOptionService.UpdateOptions(dbSubTask, model.AnswerSubTaskOptions);
            }
            else if (model.Type == SubTaskType.Code)
            {
                dbSubTask.ReporterCode = model.ReporterCode;
                dbSubTask.UnitTestsCode = model.UnitTestsCode;
            }
        }

        private async Task<int> GetNextOrder(Guid companyId, int TaskHeadId)
        {
            var TaskHead = await _TaskHeadService.GetById(companyId, TaskHeadId);
            var lastSubTask = TaskHead.SubTasks.LastOrDefault();
            return lastSubTask?.Order + 1 ?? 0;
        }
    }
}