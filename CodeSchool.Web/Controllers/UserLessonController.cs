using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.Web.Models.Lessons;
using CodeSchool.Web.Infrastructure;

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
        [Route("[action]/{userId}/{userLessonId}")]
        public async Task<IActionResult> GetById(Guid userId, int userLessonId)
        {
            var userlesson = await _userLessonService.GetById(userId, userLessonId);
            return Ok(Mapper.Map<UserLessonResponseModel>(userlesson));
        }

        [HttpGet]
        [Route("[action]/{userId}/{userChapterId}")]
        public async Task<IActionResult> GetByChapter(Guid userId, int userChapterId)
        {
            var userlessons = await _userLessonService.Get(userId, userChapterId);
            return Ok(userlessons.Select(l => new { id = l.Id, isPassed = l.IsPassed }));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UserLessonRequestResponseModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _userLessonService.Update(new UserLesson()
            {
                Id = model.Id,
                Code = model.Code,
                IsPassed = model.IsPassed,
                UserId = model.UserId,
                UserChapterId = model.UserChapterId
            });
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CanOpen([FromBody] CanOpenLessonRequestModel model)
        {
            return Ok(await _userLessonService.CanOpen(model.UserId, model.UserChapterId, model.UserLessonId));
        }
    }
}