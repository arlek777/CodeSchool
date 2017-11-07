using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IChapterService
    {
        Task<IEnumerable<Chapter>> GetChapters(Expression<Func<Chapter, bool>> predicate = null);
        Task<Chapter> GetById(int chapterId);
        Task<Chapter> AddOrUpdate(Chapter chapter);
        Task Remove(int id);
        Task ChangeOrder(int upChapterId, int downChapterId);
    }
}