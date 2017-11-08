using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using CodeSchool.Web.Models.Lessons;
using Microsoft.AspNetCore.Authorization;

namespace CodeSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IUserLessonService _userLessonService;

        public LessonController(ILessonService lessonService, IUserLessonService userLessonService)
        {
            _lessonService = lessonService;
            _userLessonService = userLessonService;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var lesson = await _lessonService.GetById(id);
            return Ok(Mapper.Map<LessonModel>(lesson));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] LessonModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

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

            if (model.Id == 0)
            {
                await _userLessonService.AddToAllUsers(lesson.Id, model.ChapterId);
            }
            else
            {
                await _userLessonService.UpdateCode(lesson.Id, model.StartCode);
            }

            model.Order = lesson.Order;
            model.Id = lesson.Id;
            return Ok(model);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] RemoveRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userLessonService.Remove(model.Id);
            await _lessonService.Remove(model.Id);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangeOrder([FromBody] ChangeOrderModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _lessonService.ChangeOrder(model.CurrentId, model.ToSwapId);
            return Ok();
        }
    }
}
