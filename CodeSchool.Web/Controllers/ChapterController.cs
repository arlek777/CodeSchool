using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Services;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class ChapterController : Controller
    {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get()
        {
            var chapters = await _chapterService.GetChapters();
            return Ok(chapters.Select(c => new ChapterModel(c)).ToList());
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] ChapterModel model)
        {
            if (!ModelState.IsValid && ModelState.Any())
            {
                return BadRequest(ModelState.FirstOrDefault().Value?.Errors?.FirstOrDefault());
            }

            var chapter = await _chapterService.AddOrUpdate(new Chapter()
            {
                Id = model.Id,
                Title = model.Title
            });

            return Ok(new ChapterModel(chapter));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] RemoveRequestModel model)
        {
            if (!ModelState.IsValid && ModelState.Any())
            {
                return BadRequest(ModelState.FirstOrDefault().Value?.Errors?.FirstOrDefault());
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
                return BadRequest(ModelState.FirstOrDefault().Value?.Errors?.FirstOrDefault());
            }

            await _chapterService.ChangeOrder(model.CurrentId, model.ToSwapId);
            return Ok();
        }
    }
}