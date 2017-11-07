using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IUserLessonService
    {
        Task<ICollection<UserLesson>> Get(Guid userId, int userChapterId);
        Task<ICollection<UserLesson>> GetOrdered(Guid userId, int userChapterId);
        Task<UserLesson> GetById(Guid userId, int userLessonId);
        Task<UserLesson> Update(UserLesson model);
        Task AddToAllUsers(int lessonId, int userChapterId);
    }
}