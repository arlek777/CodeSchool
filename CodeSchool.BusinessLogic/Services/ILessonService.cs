using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public interface ILessonService
    {
        Task<Lesson> GetById(int id);
        Task<Lesson> AddOrUpdate(Lesson model);
        Task Remove(int id);
        Task ChangeOrder(int upLessonId, int downLessonId);
    }
}