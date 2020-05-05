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
        Task<int> AddChapterLessons(Guid userId, Guid companyId, int chapterId, double timeLimit);
        Task<UserChapter> AddOnlyChapter(Guid userId, Guid companyId, int chapterId, double taskDurationLimit);
        Task<bool> CanOpen(Guid userId, int userChapterId);

        Task<UserChapterLessonInfo> GetFirstChapterAndLesson(Guid userId);
        Task<bool> StartUserTask(Guid userId);
        Task FinishUserTask(Guid userId);
    }
}