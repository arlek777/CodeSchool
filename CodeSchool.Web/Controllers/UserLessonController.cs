using System;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.BusinessLogic.Services;

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
            return Ok(chapters.Select(s => new UserChapterModel(s)));
        }

        [HttpGet]
        [Route("[action]/{userId}/{chapterId}")]
        public async Task<IActionResult> GetLatestLesson(Guid userId, int chapterId)
        {
            var latest = await _userLessonService.GetLatestLesson(userId, chapterId);
            return Ok(new UserLessonModel(latest));
        }

        [HttpGet]
        [Route("[action]/{userId}/{chapterId}")]
        public async Task<IActionResult> GetLesson(Guid userId, int lessonId)
        {
            var userlesson = await _userLessonService.GetById(userId, lessonId);
            return Ok(new UserLessonModel(userlesson));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateLesson([FromBody] UserLessonModel model)
        {
            var userlesson = await _userLessonService.UpdateLesson(new UserLesson()
            {
                Code = model.Code,
                LessonId = model.LessonId,
                IsPassed = model.IsPassed,
                UserId = model.UserId,
                UserChapterId = model.UserChapterId
            });
            return Ok(new UserLessonModel(userlesson));
        }
    }
}