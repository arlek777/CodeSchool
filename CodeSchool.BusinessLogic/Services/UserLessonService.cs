using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Extensions;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.BusinessLogic.Models;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class UserLessonService : IUserLessonService
    {
        private readonly IGenericRepository _repository;
        private readonly IUserChapterService _userChapterService;

        public UserLessonService(IGenericRepository repository, IUserChapterService userChapterService)
        {
            _repository = repository;
            _userChapterService = userChapterService;
        }

        public async Task<ICollection<UserLesson>> GetUserLessonsById(Guid userId, int userChapterId)
        {
            return (await _repository.Where<UserLesson>(c => c.UserId == userId && c.UserChapterId == userChapterId))
                .OrderBy(l => l.Lesson.Order)
                .ToList();
        }

        public async Task<UserLesson> GetUserLessonById(Guid userId, int userLessonId)
        {
            return await _repository.Find<UserLesson>(c => c.Id == userLessonId && c.UserId == userId);
        }

        public async Task Add(Guid userId, int lessonId, int chapterId, TimeSpan taskDurationLimit)
        {
            var user = await _repository.Find<User>(u => u.Id == userId);
            var userChapter = await _userChapterService.GetUserChapterByChapterId(user.Id, chapterId);
            if (userChapter == null)
            {
                userChapter = await _userChapterService.AddOnlyChapter(userId, user.CompanyId, chapterId);
            }

            _repository.Add(new UserLesson()
            {
                UserId = user.Id,
                UserChapterId = userChapter.Id,
                LessonId = lessonId,
                UpdatedDt = DateTime.UtcNow,
                TaskDurationLimit = taskDurationLimit.ToString()
            });
            await _repository.SaveChanges();
        }

        public async Task<UserLesson> Update(UserLesson model)
        {
            var userLesson = await GetUserLessonById(model.UserId, model.Id);
            userLesson.IsPassed = model.IsPassed;
            userLesson.UpdatedDt = DateTime.UtcNow;

            switch (userLesson.Lesson.Type)
            {
                case LessonType.Code:
                    userLesson.Code = model.Code;
                    break;
                case LessonType.LongAnswer:
                    userLesson.Score = model.Score;
                    break;
                case LessonType.Test:
                    userLesson.SelectedAnswerOptionId = model.SelectedAnswerOptionId;
                    break;
            }


            userLesson.UserChapter.IsPassed = (await GetUserLessonsById(model.UserId, model.UserChapterId))
                .All(l => l.IsPassed);

            await _repository.SaveChanges();

            return userLesson;
        }

        public async Task<bool> CanOpen(Guid userId, int userChapterId, int userLessonId)
        {
            var userLessons = await GetUserLessonsById(userId, userChapterId);
            var user = await _repository.Find<User>(u => u.Id == userId);
         
            var canOpenLesson = new CanOpenLesson
            {
                CanOpen = false,
                UserLessonId = userLessonId,
                UserLessons = userLessons,
                User = user
            };

            canOpenLesson = canOpenLesson
                .CheckType()
                .CheckFirst()
                .CheckOnPassed()
                .CheckAllPreviousPassed();

            return canOpenLesson.CanOpen;
        }
    }
}