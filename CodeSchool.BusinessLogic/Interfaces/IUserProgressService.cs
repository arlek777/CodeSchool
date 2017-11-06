using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public interface IUserLessonService
    {
        Task<ICollection<UserLesson>> GetByChapter(Guid userId, int userChapterId);
        Task<UserLesson> GetById(Guid userId, int userLessonId);
        Task<UserLesson> Update(UserLesson model);
        Task AddToAllUsers(int lessonId, int userChapterId);
    }
}