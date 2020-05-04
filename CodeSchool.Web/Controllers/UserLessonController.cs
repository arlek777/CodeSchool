using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.Web.Models.Lessons;
using CodeSchool.Web.Infrastructure;
using CodeSchool.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace CodeSchool.Web.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    public class UserLessonController : Controller
    {
        private readonly IUserLessonService _userLessonService;
        private readonly IUserChapterService _userChapterService;

        public UserLessonController(IUserLessonService userLessonService, IUserChapterService userChapterService)
        {
            _userLessonService = userLessonService;
            _userChapterService = userChapterService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(int userLessonId, Guid userId)
        {
            var userLesson = await _userLessonService.GetUserLessonById(userId, userLessonId);
            var mappedLesson = Mapper.Map<UserLessonModel>(userLesson);
            mappedLesson.Lesson.Answer = string.Empty;

            if (!string.IsNullOrWhiteSpace(userLesson.UserChapter.TaskDurationLimit))
            {
                mappedLesson.TimeLimit = TimeSpan.Parse(userLesson.UserChapter.TaskDurationLimit).TotalMilliseconds;
            }

            return Ok(mappedLesson);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUserLessonIds(int userChapterId , Guid userId)
        {
            var userlessons = await _userLessonService.GetUserLessonsById(userId, userChapterId);
            return Ok(userlessons.Select(l => new { id = l.Id, isPassed = l.IsPassed }));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateUserLessonModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _userLessonService.Update(new UserLesson()
            {
                Id = model.Id,
                Code = model.Code,
                SelectedAnswerOptionId = model.SelectedAnswerOptionId,
                Score = model.Score,
                IsPassed = model.IsPassed,
                UserId = model.UserId,
                UserChapterId = model.UserChapterId
            });
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CanOpen([FromBody] CanOpenLessonModel model)
        {
            var canOpenChapter = await _userChapterService.CanOpen(model.UserId, model.UserChapterId);
            return Ok(canOpenChapter);
            if (canOpenChapter)
            {
                var canOpenLesson = await _userLessonService.CanOpen(model.UserId, model.UserChapterId, model.UserLessonId);
                return Ok(canOpenLesson);
            }

            return Ok(false);
        }
    }
}