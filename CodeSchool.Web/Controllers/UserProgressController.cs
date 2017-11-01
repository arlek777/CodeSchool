using System;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Services;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserProgressController : Controller
    {
        private readonly IUserProgressService _progressService;

        public UserProgressController(IUserProgressService progressService)
        {
            _progressService = progressService;
        }

        [HttpGet]
        [Route("[action]/{userId}")]
        public async Task<IActionResult> GetSummary(Guid userId)
        {
            var summary = await _progressService.GetProgressSummary(userId);
            return Ok(summary.Select(s => new UserChapterProgressModel(s)));
        }

        [HttpGet]
        [Route("[action]/{userId}/{chapterId}")]
        public async Task<IActionResult> GetLatestLesson(Guid userId, int chapterId)
        {
            var latest = await _progressService.GetLatestLesson(userId, chapterId);
            return Ok(new UserLessonProgressModel(latest));
        }

        [HttpGet]
        [Route("[action]/{userId}/{chapterId}")]
        public async Task<IActionResult> GetLessonProgress(Guid userId, int lessonId)
        {
            var progress = await _progressService.GetLessonProgress(userId, lessonId);
            return Ok(new UserLessonProgressModel(progress));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateOrUpdate([FromBody] UserLessonProgressModel model)
        {
            var progress = await _progressService.CreateOrUpdateLessonProgress(new UserLessonProgress()
            {
                Code = model.Code,
                LessonId = model.LessonId,
                IsPassed = model.IsPassed,
                UserId = model.UserId,
                UserChapterProgressId = model.UserChapterProgressId
            });
            return Ok(new UserLessonProgressModel(progress));
        }
    }
}