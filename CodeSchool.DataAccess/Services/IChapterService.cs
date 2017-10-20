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
    }
}