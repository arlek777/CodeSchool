using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.BusinessLogic.Services;
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
        [Route("[action]/{userId}/{lessonId}")]
        public async Task<IActionResult> GetById(Guid userId, int lessonId)
        {
            var userlesson = await _userLessonService.GetById(userId, lessonId);
            return Ok(Mapper.Map<UserLessonModel>(userlesson));
        }

        [HttpGet]
        [Route("[action]/{userId}/{chapterId}")]
        public async Task<IActionResult> GetIdsByChapter(Guid userId, int chapterId)
        {
            var userlessons = await _userLessonService.GetOrdered(userId, chapterId);
            return Ok(userlessons.Select(l => l.Id));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UserLessonUpdateModel model)
        {
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