using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public interface IUserChapterService
    {
        Task<ICollection<UserChapter>> Get(Guid userId);
        Task<UserLesson> GetLatestLesson(Guid userId, int userChapterId);
        Task AddToAllUsers(int chapterId);
        Task Add(Guid userId);
    }
}