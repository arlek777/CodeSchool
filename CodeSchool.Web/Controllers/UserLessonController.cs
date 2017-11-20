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
        [Route("[action]/{userId}/{lessonId}")]
        public async Task<IActionResult> GetById(Guid userId, int lessonId)
        {
            var userlesson = await _userLessonService.GetById(userId, lessonId);
            return Ok(Mapper.Map<UserLessonResponseModel>(userlesson));
        }

        [HttpGet]
        [Route("[action]/{userId}/{chapterId}")]
        public async Task<IActionResult> GetByChapter(Guid userId, int chapterId)
        {
            var userlessons = await _userLessonService.Get(userId, chapterId);
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
    }
}