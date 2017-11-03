using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public interface IUserChapterService
    {
        Task<ICollection<UserChapter>> GetUserChapters(Guid userId);
        Task<UserLesson> GetLatestLesson(Guid userId, int userChapterId);
        Task AddUserChapterToAllUsers(int chapterId);
        Task AddChaptersForNewUser(Guid userId);
    }
}