using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.BusinessLogic.Models;
using CodeSchool.Web.Models.Chapters;
using Microsoft.AspNetCore.Authorization;
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
        [Route("[action]")]
        public async Task<IActionResult> Get(FilterUserChapterModel model)
        {
            var chapters = await _chapterService.GetUserChapters(model);
            return Ok(chapters.Select(Mapper.Map<UserChapterShortcutModel>));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CanOpen([FromBody] CanOpenChapterModel model)
        {
            return Ok(await _chapterService.CanOpen(model.UserId, model.UserChapterId));
        }
    }
}