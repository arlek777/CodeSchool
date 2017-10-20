using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.DataAccess.Services;
using CodeSchool.Domain;
using CodeSchool.Web.Models;

namespace CodeSchool.Web.Controllers
{
    [Route("api/[controller]")]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var lesson = await _lessonService.Get(id);
            return Ok(new LessonModel(lesson));
        }

        [HttpGet]
        [Route("[action]/{chapterId}/{id}")]
        public async Task<IActionResult> GetNext(int chapterId, int id)
        {
            var nextLesson = await _lessonService.GetNext(chapterId, id);
            return Ok(new LessonModel(nextLesson));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] LessonModel model)
        {
            var lesson = await _lessonService.AddOrUpdate(new Lesson()
            {
                Id = model.Id,
                ChapterId = model.ChapterId,
                Title = model.Title,
                Text = model.Text,
                StartCode = model.StartCode,
                ReporterCode = model.ReporterCode,
                UnitTestsCode = model.UnitTestsCode
            });

            return Ok(new LessonModel(lesson));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] int id)
        {
            await _lessonService.Remove(id);
            return Ok();
        }
    }
}
