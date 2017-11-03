using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public interface IUserLessonService
    {
        Task<ICollection<UserLesson>> GetUserLessonsByChapter(Guid userId, int userChapterId);
        Task<UserLesson> GetLessonById(Guid userId, int userLessonId);
        Task<UserLesson> UpdateLesson(UserLesson model);
        Task AddUserLessonToAllUsers(int lessonId, int chapterId);
    }
}