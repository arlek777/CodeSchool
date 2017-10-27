using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.DataAccess.Services
{
    public interface IChapterService
    {
        Task<IEnumerable<Chapter>> GetShortcutChapters();
        Task Remove(int id);
        Task<Chapter> AddOrUpdate(Chapter chapter);
        Task<Chapter> GetNext(int chapterId);
        Task ChangeOrder(int upChapterId, int downChapterId);
        Task<Chapter> Get(int chapterId);
    }
}