using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public interface IChapterService
    {
        Task<IEnumerable<Chapter>> GetChapters();
        Task<Chapter> GetById(int chapterId);
        Task<Chapter> AddOrUpdate(Chapter chapter);
        Task Remove(int id);
        Task ChangeOrder(int upChapterId, int downChapterId);
    }
}