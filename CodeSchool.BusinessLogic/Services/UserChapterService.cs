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
            var userChapters = (await _repository.Where<UserChapter>(c => c.UserId == userId))
                .OrderBy(c => c.Chapter.Order)
                .ToList();

            userChapters.ForEach(c => c.UserLessons = c.UserLessons.OrderBy(l => l.Lesson.Order).ToList());
            return userChapters;
        }

        public async Task<UserChapter> GetByChapterId(Guid userId, int chapterId)
        {
            var userChapters = await Get(userId);
            var userChapter = userChapters.FirstOrDefault(c => c.ChapterId == chapterId);
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
            var chapters = await _chapterService.Get();

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

        public async Task Remove(int chapterId)
        {
            var chapters = (await _repository.Where<UserChapter>(c => c.ChapterId == chapterId)).ToList();

            for (var i = 0; i < chapters.Count; i++)
            {
                var userLessons = chapters[i].UserLessons;
                while (userLessons.Any())
                {
                    _repository.Remove(userLessons.ElementAt(0));
                    await _repository.SaveChanges();
                }

                _repository.Remove(chapters[i]);
                await _repository.SaveChanges();
            }
        }

        public async Task<bool> CanOpen(Guid userId, int userChapterId)
        {
            var userChapters = (await Get(userId)).ToList();
            if (!userChapters.Any()) return false;

            var result = CheckFirst(new { canOpen = false, userChapters, userChapterId });
            result = CheckOnPassed(result);
            result = CheckAllPreviousPassed(result);

            return result.canOpen;
        }

        private dynamic CheckFirst(dynamic result)
        {
            if (result.canOpen) return result;

            var firstChapter = result.userChapters.FirstOrDefault();
            var isFirst = firstChapter != null && firstChapter.Id == result.userChapterId;
            return new
            {
                canOpen = isFirst,
                result.userChapters,
                result.userChapterId
            };
        }

        private dynamic CheckOnPassed(dynamic result)
        {
            if (result.canOpen) return result;

            var userChapters = (ICollection<UserChapter>)result.userChapters;
            var userChapter = userChapters.FirstOrDefault(c => c.Id == result.userChapterId);
            return new
            {
                canOpen = userChapter != null && userChapter.IsPassed,
                result.userChapters,
                result.userChapterId
            };
        }

        private dynamic CheckAllPreviousPassed(dynamic result)
        {
            if (result.canOpen) return result;

            var userChapters = (ICollection<UserChapter>)result.userChapters;
            var allPreviousPassed = userChapters.TakeWhile(c => c.Id != result.userChapterId).All(c => c.IsPassed);
            return new
            {
                canOpen = allPreviousPassed,
                result.userChapters,
                result.userChapterId
            };
        }
    }
}