using System;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface ILessonService
    {
        Task<Lesson> GetById(Guid companyId, int id);
        Task<Lesson> AddOrUpdate(Lesson model);
        Task Remove(Guid companyId, int id);
        Task ChangeOrder(Guid companyId, int upLessonId, int downLessonId);
    }
}