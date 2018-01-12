using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IChapterService
    {
        Task<IEnumerable<Chapter>> GetChapters(int? categoryId = null);
        Task<Chapter> GetById(int chapterId);
        Task<Chapter> AddOrUpdate(Chapter chapter);
        Task Remove(int id);
        Task ChangeOrder(int upChapterId, int downChapterId);
    }
}