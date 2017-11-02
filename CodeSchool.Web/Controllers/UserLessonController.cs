using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.BusinessLogic.Services;
using CodeSchool.Web.Models.Chapters;
using CodeSchool.Web.Models.Lessons;

namespace CodeSchool.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserLessonController : Controller
    {
        private readonly IUserLessonService _userLessonService;

        public UserLessonController(IUserLessonService userLessonService)
        {
            _userLessonService = userLessonService;
        }

        [HttpGet]
        [Route("[action]/{userId}")]
        public async Task<IActionResult> GetUserChapters(Guid userId)
        {
            var chapters = await _userLessonService.GetUserChapters(userId);

            var chapterShortcuts = chapters.Select(c =>
            {
                var shortcut = Mapper.Map<UserChapterShortcutModel>(c);
                shortcut.UserLessons = new LinkedList<UserLessonShortcutModel>(
                    shortcut.UserLessons.OrderBy(l => l.LessonOrder));
                return shortcut;
            }).OrderBy(c => c.ChapterOrder);

            return Ok(chapterShortcuts);
        }

        [HttpGet]
        [Route("[action]/{userId}/{chapterId}")]
        public async Task<IActionResult> GetLatestLesson(Guid userId, int chapterId)
        {
            var latest = await _userLessonService.GetLatestLesson(userId, chapterId);
            return Ok(Mapper.Map<UserLessonModel>(latest));
        }

        [HttpGet]
        [Route("[action]/{userId}/{lessonId}")]
        public async Task<IActionResult> GetLesson(Guid userId, int lessonId)
        {
            var userlesson = await _userLessonService.GetLessonById(userId, lessonId);
            return Ok(Mapper.Map<UserLessonModel>(userlesson));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateLesson([FromBody] UserLessonModel model)
        {
            var userlesson = await _userLessonService.UpdateLesson(new UserLesson()
            {
                Id = model.Id,
                Code = model.Code,
                LessonId = model.LessonId,
                IsPassed = model.IsPassed,
                UserId = model.UserId,
                UserChapterId = model.UserChapterId
            });
            return Ok(Mapper.Map<UserLessonModel>(userlesson));
        }
    }
}