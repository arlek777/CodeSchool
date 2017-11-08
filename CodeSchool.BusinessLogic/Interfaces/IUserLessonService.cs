using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IUserLessonService
    {
        Task<ICollection<UserLesson>> Get(Guid userId, int userChapterId);
        Task<UserLesson> GetById(Guid userId, int userLessonId);
        Task AddToAllUsers(int lessonId, int userChapterId);
        Task<UserLesson> Update(UserLesson model);
        Task UpdateCode(int lessonId, string code);
        Task Remove(int lessonId);
    }
}