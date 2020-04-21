using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IChapterService
    {
        Task<IEnumerable<Chapter>> GetChapters(Guid companyId, int? categoryId = null);
        Task<Chapter> GetById(Guid companyId, int chapterId);
        Task<Chapter> AddOrUpdate(Chapter chapter);
        Task Remove(Guid companyId, int id);
        Task ChangeOrder(Guid companyId, int upChapterId, int downChapterId);
    }
}