using System.Linq;
using System.Threading.Tasks;
using CodeSchool.DataAccess.Services;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
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
            var chapters = await _chapterService.GetShortcutChapters();
            return Ok(chapters.Select(c => new ChapterModel(c)).ToList());
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] ChapterModel model)
        {
            var chapter = await _chapterService.AddOrUpdate(new Chapter()
            {
                Id = model.Id,
                Title = model.Title
            });

            return Ok(new ChapterModel(chapter));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] int id)
        {
            await _chapterService.Remove(id);
            return Ok();
        }
    }
}