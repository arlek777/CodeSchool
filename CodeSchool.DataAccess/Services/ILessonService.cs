using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.DataAccess.Services
{
    public interface ILessonService
    {
        Task<Lesson> Get(int id);
        Task<Lesson> GetNext(int chapterId, int id);
    }
}