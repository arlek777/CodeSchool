using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IUserChapterService
    {
        Task<ICollection<UserChapter>> Get(Guid userId);
        Task<UserChapter> GetById(Guid userId, int userChapterId);
        Task AddToAllUsers(int chapterId);
        Task Add(Guid userId);
    }
}