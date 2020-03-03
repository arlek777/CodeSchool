using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface ILessonService
    {
        Task<Lesson> GetById(string companyId, int id);
        Task<Lesson> AddOrUpdate(Lesson model);
        Task Remove(string companyId, int id);
        Task ChangeOrder(string companyId, int upLessonId, int downLessonId);
    }
}