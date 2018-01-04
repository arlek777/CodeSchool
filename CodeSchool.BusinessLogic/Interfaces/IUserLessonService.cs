using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IUserLessonService
    {
        Task<ICollection<UserLesson>> GetUserLessonsById(Guid userId, int userChapterId);
        Task<UserLesson> GetUserLessonById(Guid userId, int userLessonId);
        Task AddToAllUsers(int lessonId, int chapterId);
        Task<UserLesson> Update(UserLesson model);
        Task Remove(int lessonId);
        Task<bool> CanOpen(Guid userId, int userChapterId, int userLessonId);
    }
}