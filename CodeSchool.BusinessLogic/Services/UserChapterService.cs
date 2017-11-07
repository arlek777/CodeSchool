using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class UserChapterService : IUserChapterService
    {
        private readonly IGenericRepository _repository;
        private readonly IChapterService _chapterService;

        public UserChapterService(IGenericRepository repository, IChapterService chapterService)
        {
            _repository = repository;
            _chapterService = chapterService;
        }

        public async Task<ICollection<UserChapter>> Get(Guid userId)
        {
            var userChapters = (await _repository
                .Where<UserChapter>(c => c.UserId == userId && !c.Chapter.IsRemoved))
                .OrderBy(c => c.Chapter.Order).ToList();

            userChapters.ForEach(c => c.UserLessons =
                c.UserLessons.Where(l => !l.Lesson.IsRemoved).OrderBy(l => l.Lesson.Order).ToList());

            return userChapters;
        }

        public async Task<UserChapter> GetById(Guid userId, int userChapterId)
        {
            var userChapter = (await Get(userId)).FirstOrDefault(c => c.Id == userChapterId);
            return userChapter;
        }

        public async Task AddToAllUsers(int chapterId)
        {
            var users = await _repository.GetAll<User>();
            foreach (var user in users)
            {
                var userChapter = new UserChapter()
                {
                    UserId = user.Id,
                    ChapterId = chapterId
                };

                _repository.Add(userChapter);
            }

            await _repository.SaveChanges();
        }

        public async Task Add(Guid userId)
        {
            var chapters = await _chapterService.GetChapters();

            foreach (var chapter in chapters)
            {
                var newChapter = new UserChapter()
                {
                    ChapterId = chapter.Id,
                    UserId = userId
                };
                _repository.Add(newChapter);
                await _repository.SaveChanges();

                foreach (var lesson in chapter.Lessons)
                {
                    var newLesson = new UserLesson()
                    {
                        UserChapterId = newChapter.Id,
                        UserId = userId,
                        Code = lesson.StartCode,
                        LessonId = lesson.Id,
                        UpdatedDt = DateTime.UtcNow
                    };

                    _repository.Add(newLesson);
                }

                await _repository.SaveChanges();
            }
        }
    }
}