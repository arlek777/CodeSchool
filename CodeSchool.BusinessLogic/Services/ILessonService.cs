using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public interface ILessonService
    {
        Task<Lesson> Get(int id);
        Task<Lesson> GetNext(int chapterId, int id);
        Task<Lesson> AddOrUpdate(Lesson model);
        Task Remove(int id);
        Task ChangeOrder(int upLessonId, int downLessonId);
    }
}