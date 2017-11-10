using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IUserChapterService
    {
        Task<ICollection<UserChapter>> Get(Guid userId);
        Task<UserChapter> GetByChapterId(Guid userId, int chapterId);
        Task AddToAllUsers(int chapterId);
        Task Add(Guid userId);
        Task Remove(int chapterId);
    }
}