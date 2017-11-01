using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace CodeSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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
            var lesson = await _lessonService.GetById(id);
            return Ok(new LessonModel(lesson));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] LessonModel model)
        {
            if (!ModelState.IsValid && ModelState.Any())
            {
                return BadRequest(ModelState.FirstOrDefault().Value?.Errors?.FirstOrDefault());
            }

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
        public async Task<IActionResult> Remove([FromBody] RemoveRequestModel model)
        {
            if (!ModelState.IsValid && ModelState.Any())
            {
                return BadRequest(ModelState.FirstOrDefault().Value?.Errors?.FirstOrDefault());
            }

            await _lessonService.Remove(model.Id);
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

            await _lessonService.ChangeOrder(model.CurrentId, model.ToSwapId);
            return Ok();
        }
    }
}
