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
        private readonly ILessonService _lessonService;

        public UserLessonService(IGenericRepository repository, 
            IChapterService chapterService, ILessonService lessonService)
        {
            _repository = repository;
            _chapterService = chapterService;
            _lessonService = lessonService;
        }

        public async Task<ICollection<UserChapter>> GetUserChapters(Guid userId)
        {
            var userChapters = await _repository.Where<UserChapter>(c => c.UserId == userId);
            return userChapters;
        }

        public async Task<ICollection<UserLesson>> GetUserLessonsByChapter(Guid userId, int userChapterId)
        {
            var userLessons = await _repository.Where<UserLesson>(c => c.UserId == userId && c.UserChapterId == userChapterId);
            return userLessons;
        }

        public async Task<UserLesson> GetLatestLesson(Guid userId, int userChapterId)
        {
            var chapterProgress =
                await _repository.Find<UserChapter>(c => c.Id == userChapterId && c.UserId == userId);

            return chapterProgress?.UserLessons.OrderBy(l => l.UpdatedDt).LastOrDefault();
        }

        public async Task<UserLesson> GetLessonById(Guid userId, int userLessonId)
        {
            var userLesson =
                await _repository.Find<UserLesson>(c => c.Id == userLessonId && c.UserId == userId);

            return userLesson;
        }

        public async Task AddUserChapterToAllUsers(int chapterId)
        {
            var users = await _repository.GetAll<User>();
            foreach(var user in users)
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

        public async Task AddUserLessonToAllUsers(int lessonId, int chapterId)
        {
            var users = await _repository.GetAll<User>();
            foreach (var user in users)
            {
                var userChapter = await _repository.Find<UserChapter>(c => c.UserId == user.Id && c.ChapterId == chapterId);
                var lesson = await _lessonService.GetById(lessonId);

                _repository.Add(new UserLesson()
                {
                    UserId = user.Id,
                    UserChapterId = userChapter.Id,
                    LessonId = lessonId,
                    Code = lesson.StartCode,
                    UpdatedDt = DateTime.UtcNow
                });
            }
            await _repository.SaveChanges();
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

                    _repository.Add(newLesson);
                }

                await _repository.SaveChanges();
            }
        }

        public async Task<UserLesson> UpdateLesson(UserLesson model)
        {
            var userLesson = await GetLessonById(model.UserId, model.Id);
            userLesson.IsPassed = model.IsPassed;
            userLesson.UpdatedDt = DateTime.UtcNow;
            userLesson.Code = model.Code;

            await _repository.SaveChanges();

            return userLesson;
        }
    }
}