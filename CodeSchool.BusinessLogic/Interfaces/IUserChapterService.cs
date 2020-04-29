using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Models;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IUserChapterService
    {
        Task<ICollection<UserChapter>> GetUserChaptersByUserId(Guid userId);
        Task<ICollection<UserChapter>> GetUserChapters(FilterUserChapterModel filterModel);
        Task<UserChapter> GetUserChapterByChapterId(Guid userId, int chapterId);
        Task AddChapterLessons(Guid userId, Guid companyId, int chapterId);
        Task<UserChapter> AddOnlyChapter(Guid userId, Guid companyId, int chapterId);
        Task<bool> CanOpen(Guid userId, int userChapterId);
        Task<StartUserTask> StartUserTask(Guid userId);
    }
}