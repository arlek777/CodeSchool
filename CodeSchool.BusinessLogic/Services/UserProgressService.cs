using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class UserProgressService : IUserProgressService
    {
        private readonly IGenericRepository _repository;

        public UserProgressService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<UserChapterProgress>> GetProgressSummary(Guid userId)
        {
            var userProgresses = (await _repository
                    .Where<UserChapterProgress>(c => c.UserId == userId))
                .Select(c => new UserChapterProgress()
                {
                    Id = c.Id,
                    ChapterId = c.ChapterId,
                    IsPassed = c.IsPassed,
                    UserId = c.UserId,
                    UserLessonProgresses = c.UserLessonProgresses.Select(l => new UserLessonProgress()
                    {
                        Id = l.Id,
                        IsPassed = l.IsPassed,
                        LessonId = l.LessonId,
                        UserId = l.UserId
                    }).ToList() 
                }).ToList();

            return userProgresses;
        }

        public async Task<UserLessonProgress> GetLatestLesson(Guid userId, int chapterId)
        {
            var chapterProgress =
                await _repository.Find<UserChapterProgress>(c => c.ChapterId == chapterId && c.UserId == userId);

            return chapterProgress?.UserLessonProgresses.OrderBy(l => l.UpdatedDt).LastOrDefault();
        }

        public async Task<UserLessonProgress> GetLessonProgress(Guid userId, int lessonId)
        {
            var lessonProgress =
                await _repository.Find<UserLessonProgress>(c => c.LessonId == lessonId && c.UserId == userId);

            return lessonProgress;
        }

        public async Task<UserLessonProgress> CreateOrUpdateLessonProgress(UserLessonProgress model)
        {
            var lessonProgress = await GetLessonProgress(model.UserId, model.LessonId);
            if (lessonProgress != null)
            {
                lessonProgress.IsPassed = model.IsPassed;
                lessonProgress.UpdatedDt = DateTime.UtcNow;
                lessonProgress.Code = model.Code;
            }
            else
            {
                lessonProgress = await AddLessonProgress(model);
            }

            await _repository.SaveChanges();

            return lessonProgress;
        }

        private async Task<UserLessonProgress> AddLessonProgress(UserLessonProgress model)
        {
            var chapterProgress = await _repository.Find<UserChapterProgress>(c => c.Id == model.UserChapterProgressId) ??
                                  await CreateChapterProgress(model);

            var lessonProgress = new UserLessonProgress()
            {
                Code = model.Code,
                IsPassed = model.IsPassed,
                UpdatedDt = DateTime.UtcNow,
                LessonId = model.LessonId,
                UserId = model.UserId,
                UserChapterProgressId = chapterProgress.Id
            };

            _repository.Add(lessonProgress);

            return lessonProgress;
        }

        private async Task<UserChapterProgress> CreateChapterProgress(UserLessonProgress model)
        {
            var lesson = await _repository.Find<Lesson>(l => l.Id == model.LessonId);
            var chapterProgress = new UserChapterProgress()
            {
                UserId = model.UserId,
                ChapterId = lesson.ChapterId
            };

            _repository.Add(chapterProgress);
            await _repository.SaveChanges();

            return chapterProgress;
        }
    }
}