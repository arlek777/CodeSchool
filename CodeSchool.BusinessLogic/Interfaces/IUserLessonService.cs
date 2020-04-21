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
        Task Add(Guid userId, int lessonId, int chapterId, TimeSpan taskDurationLimit);
        Task<UserLesson> Update(UserLesson model);
        Task<bool> CanOpen(Guid userId, int userChapterId, int userLessonId);
    }
}