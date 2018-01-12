using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using CodeSchool.Web.Models.Chapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.Web.Infrastructure;

namespace CodeSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class ChapterController : Controller
    {
        private readonly IChapterService _chapterService;
        private readonly IUserChapterService _userChapterService;
        private readonly ILessonService _lessonService;

        public ChapterController(IChapterService chapterService,  
            IUserChapterService userChapterService, ILessonService lessonService)
        {
            _chapterService = chapterService;
            _userChapterService = userChapterService;
            _lessonService = lessonService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get()
        {
            var chapters = await _chapterService.GetChapters();
            return Ok(chapters.Select(Mapper.Map<ChapterShortcutModel>));
        }

        [HttpGet]
        [Route("[action]/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var chapters = await _chapterService.GetChapters(categoryId);
            return Ok(chapters.Select(Mapper.Map<ChapterShortcutModel>));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] ChapterShortcutModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var chapter = await _chapterService.AddOrUpdate(Mapper.Map<Chapter>(model));

            if(model.Id == 0)
            {
                await _userChapterService.AddToAllUsers(chapter.Id);
            }

            model.Id = chapter.Id;
            model.Order = chapter.Order;
            return Ok(model);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] IdModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _userChapterService.RemoveAllInChapter(model.Id);
            await _lessonService.RemoveAllInChapter(model.Id);
            await _chapterService.Remove(model.Id);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangeOrder([FromBody] ChangeOrderModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _chapterService.ChangeOrder(model.CurrentId, model.ToSwapId);
            return Ok();
        }
    }
}