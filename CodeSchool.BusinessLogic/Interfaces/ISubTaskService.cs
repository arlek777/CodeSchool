using System;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface ISubTaskService
    {
        Task<SubTask> GetById(Guid companyId, int id);
        Task<SubTask> AddOrUpdate(SubTask model);
        Task Remove(Guid companyId, int id);
        Task ChangeOrder(Guid companyId, int upSubTaskId, int downSubTaskId);
    }
}