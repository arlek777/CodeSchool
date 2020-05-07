using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface ITaskHeadService
    {
        Task<IEnumerable<TaskHead>> GetTaskHeads(Guid companyId, int? categoryId = null);
        Task<TaskHead> GetById(Guid companyId, int TaskHeadId);
        Task<TaskHead> AddOrUpdate(TaskHead taskHead);
        Task Remove(Guid companyId, int id);
        Task ChangeOrder(Guid companyId, int upTaskHeadId, int downTaskHeadId);
    }
}