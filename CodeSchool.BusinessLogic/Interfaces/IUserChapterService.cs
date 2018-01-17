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
        Task AddToAllUsers(int chapterId);
        Task Add(Guid userId);
        Task<bool> CanOpen(Guid userId, int userChapterId);
    }
}