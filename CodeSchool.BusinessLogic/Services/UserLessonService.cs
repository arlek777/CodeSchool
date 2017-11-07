using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class UserLessonService : IUserLessonService
    {
        private readonly IGenericRepository _repository;
        private readonly ILessonService _lessonService;
        private readonly IUserChapterService _userChapterService;

        public UserLessonService(IGenericRepository repository, ILessonService lessonService, IUserChapterService userChapterService)
        {
            _repository = repository;
            _lessonService = lessonService;
            _userChapterService = userChapterService;
        }

        public async Task<ICollection<UserLesson>> Get(Guid userId, int userChapterId)
        {
            return (await _repository.Where<UserLesson>(c => c.UserId == userId && c.UserChapterId == userChapterId))
                .OrderBy(l => l.Lesson.Order)
                .ToList();
        }

        public async Task<UserLesson> GetById(Guid userId, int userLessonId)
        {
            return await _repository.Find<UserLesson>(c => c.Id == userLessonId && c.UserId == userId);
        }

        public async Task AddToAllUsers(int lessonId, int userChapterId)
        {
            var users = await _repository.GetAll<User>();
            foreach (var user in users)
            {
                var userChapter = await _userChapterService.GetById(user.Id, userChapterId);
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

        public async Task<UserLesson> Update(UserLesson model)
        {
            var userLesson = await GetById(model.UserId, model.Id);
            userLesson.IsPassed = model.IsPassed;
            userLesson.UpdatedDt = DateTime.UtcNow;
            userLesson.Code = model.Code;

            userLesson.UserChapter.IsPassed = (await Get(model.UserId, model.UserChapterId))
                .All(l => l.IsPassed);

            await _repository.SaveChanges();

            return userLesson;
        }

        public async Task Remove(int lessonId)
        {
            var userLessons = (await _repository.Where<UserLesson>(l => l.LessonId == lessonId)).ToList();
            for (var i = 0; i < userLessons.Count; i++)
            {
                _repository.Remove(userLessons[i]);
                await _repository.SaveChanges();
            }
        }
    }
}