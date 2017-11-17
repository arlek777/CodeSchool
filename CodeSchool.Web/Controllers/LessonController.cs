using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using CodeSchool.Web.Models.Lessons;
using Microsoft.AspNetCore.Authorization;
using CodeSchool.Web.Infrastructure;

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
            return Ok(Mapper.Map<LessonRequestResponseModel>(lesson));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] LessonRequestResponseModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var lesson = await _lessonService.AddOrUpdate(Mapper.Map<Lesson>(model));
            if (model.Id == 0)
            {
                await _userLessonService.AddToAllUsers(lesson.Id, model.ChapterId);
            }
            
            model.Order = lesson.Order;
            model.Id = lesson.Id;
            return Ok(model);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] RemoveRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _userLessonService.Remove(model.Id);
            await _lessonService.Remove(model.Id);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangeOrder([FromBody] ChangeOrderRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _lessonService.ChangeOrder(model.CurrentId, model.ToSwapId);
            return Ok();
        }
    }
}
