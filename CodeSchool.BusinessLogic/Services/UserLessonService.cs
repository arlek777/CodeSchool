using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class UserLessonService : IUserLessonService
    {
        private readonly IGenericRepository _repository;
        private readonly IChapterService _chapterService;

        public UserLessonService(IGenericRepository repository, 
            IChapterService chapterService)
        {
            _repository = repository;
            _chapterService = chapterService;
        }

        public async Task<ICollection<UserChapter>> GetUserChapters(Guid userId)
        {
            var userChapters = await _repository.Where<UserChapter>(c => c.UserId == userId);
            return userChapters;
        }

        public async Task<ICollection<UserLesson>> GetUserLessonsByChapter(Guid userId, int chapterId)
        {
            var userLessons = await _repository.Where<UserLesson>(c => c.UserId == userId && c.UserChapter.ChapterId == chapterId);
            return userLessons;
        }

        public async Task<UserLesson> GetLatestLesson(Guid userId, int chapterId)
        {
            var chapterProgress =
                await _repository.Find<UserChapter>(c => c.ChapterId == chapterId && c.UserId == userId);

            return chapterProgress?.UserLessons.OrderBy(l => l.UpdatedDt).LastOrDefault();
        }

        public async Task<UserLesson> GetById(Guid userId, int lessonId)
        {
            var userLesson =
                await _repository.Find<UserLesson>(c => c.LessonId == lessonId && c.UserId == userId);

            return userLesson;
        }

        public async Task CreateForNewUser(Guid userId)
        {
            var chapters = await _chapterService.GetChapters();
            
            foreach(var chapter in chapters)
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

                    _repository.Add(newChapter);
                }

                await _repository.SaveChanges();
            }
        }

        public async Task<UserLesson> UpdateLesson(UserLesson model)
        {
            var userLesson = await GetById(model.UserId, model.LessonId);
            userLesson.IsPassed = model.IsPassed;
            userLesson.UpdatedDt = DateTime.UtcNow;
            userLesson.Code = model.Code;

            await _repository.SaveChanges();

            return userLesson;
        }
    }
}