using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public interface IUserLessonService
    {
        Task CreateForNewUser(Guid userId);
        Task<ICollection<UserChapter>> GetUserChapters(Guid userId);
        Task<ICollection<UserLesson>> GetUserLessonsByChapter(Guid userId, int userChapterId);
        Task<UserLesson> GetLessonById(Guid userId, int userLessonId);
        Task<UserLesson> GetLatestLesson(Guid userId, int chapterId);
        Task<UserLesson> UpdateLesson(UserLesson model);
        Task AddUserLessonToAllUsers(int lessonId, int chapterId);
        Task AddUserChapterToAllUsers(int chapterId);
    }
}