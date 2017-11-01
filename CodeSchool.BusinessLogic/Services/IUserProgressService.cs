using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public interface IUserProgressService
    {
        Task<ICollection<UserChapterProgress>> GetProgressSummary(Guid userId);
        Task<UserLessonProgress> GetLatestLesson(Guid userId, int chapterId);
        Task<UserLessonProgress> GetLessonProgress(Guid userId, int lessonId);
        Task<UserLessonProgress> CreateOrUpdateLessonProgress(UserLessonProgress model);
    }
}