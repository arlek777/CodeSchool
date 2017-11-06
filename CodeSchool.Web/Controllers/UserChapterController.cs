using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Services;
using CodeSchool.Domain;
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
            var chapters = await _chapterService.Get(userId);
            return Ok(chapters.Select(Mapper.Map<UserChapterShortcutModel>));
        }
    }
}