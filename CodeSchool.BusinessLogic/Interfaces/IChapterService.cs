using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IChapterService
    {
        Task<IEnumerable<Chapter>> GetChapters(string companyId, int? categoryId = null);
        Task<Chapter> GetById(string companyId, int chapterId);
        Task<Chapter> AddOrUpdate(Chapter chapter);
        Task Remove(string companyId, int id);
        Task ChangeOrder(string companyId, int upChapterId, int downChapterId);
    }
}