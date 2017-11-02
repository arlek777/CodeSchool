using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Services;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using CodeSchool.Web.Models.Chapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class ChapterController : Controller
    {
        private readonly IChapterService _chapterService;
        private readonly IUserLessonService _userLessonService;

        public ChapterController(IChapterService chapterService, IUserLessonService userLessonService)
        {
            _chapterService = chapterService;
            _userLessonService = userLessonService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get()
        {
            var chapters = await _chapterService.GetChapters();
            var chapterShortcuts = chapters.Select(c =>
            {
                var shortCut = Mapper.Map<ChapterShortcutModel>(c);
                shortCut.Lessons = shortCut.Lessons.OrderBy(l => l.Order);
                return shortCut;
            }).OrderBy(c => c.Order);

            return Ok(chapterShortcuts);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] ChapterShortcutModel model)
        {
            if (!ModelState.IsValid && ModelState.Any())
            {
                return BadRequest(ModelState.FirstOrDefault().Value?.Errors?.FirstOrDefault()?.ErrorMessage);
            }

            var chapter = await _chapterService.AddOrUpdate(new Chapter()
            {
                Id = model.Id,
                Title = model.Title
            });

            if(model.Id == 0)
            {
                await _userLessonService.AddUserChapterToAllUsers(chapter.Id);
            }

            model.Id = chapter.Id;
            model.Order = chapter.Order;
            return Ok(model);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] RemoveRequestModel model)
        {
            if (!ModelState.IsValid && ModelState.Any())
            {
                return BadRequest(ModelState.FirstOrDefault().Value?.Errors?.FirstOrDefault()?.ErrorMessage);
            }

            await _chapterService.Remove(model.Id);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangeOrder([FromBody] ChangeOrderModel model)
        {
            if (!ModelState.IsValid && ModelState.Any())
            {
                return BadRequest(ModelState.FirstOrDefault().Value?.Errors?.FirstOrDefault()?.ErrorMessage);
            }

            await _chapterService.ChangeOrder(model.CurrentId, model.ToSwapId);
            return Ok();
        }
    }
}