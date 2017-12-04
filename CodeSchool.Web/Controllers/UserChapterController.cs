using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Web.Models.Chapters;
using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserChapterController : Controller
    {
        private readonly IUserChapterService _chapterService;

        public UserChapterController(IUserChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet]
        [Route("[action]/{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var chapters = await _chapterService.GetAll(userId);
            return Ok(chapters.Select(Mapper.Map<UserChapterShortcutResponseModel>));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CanOpen([FromBody] CanOpenChapterRequestModel model)
        {
            return Ok(await _chapterService.CanOpen(model.UserId, model.UserChapterId));
        }
    }
}