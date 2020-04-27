using System;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.Web.Models;
using CodeSchool.Web.Models.Lessons;
using Microsoft.AspNetCore.Authorization;
using CodeSchool.Web.Infrastructure.Extensions;
using CodeSchool.Web.Models.Chapters;

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
        public async Task<IActionResult> Get(Guid companyId, int id)
        {
            var lesson = await _lessonService.GetById(companyId, id);
            return Ok(Mapper.Map<LessonModel>(lesson));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] LessonModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var lesson = await _lessonService.AddOrUpdate(Mapper.Map<Lesson>(model));
            if (model.PublishNow)
            {
                await PublishLesson(new PublishLessonModel
                {
                    CompanyId = lesson.CompanyId,
                    ChapterId = lesson.ChapterId,
                    LessonId = lesson.Id
                });
            }
            
            model.Order = lesson.Order;
            model.Id = lesson.Id;
            return Ok(model);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Publish([FromBody] PublishLessonModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await PublishLesson(model);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] RemoveModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());
            await _lessonService.Remove(model.CompanyId, model.Id);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangeOrder([FromBody] ChangeOrderModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _lessonService.ChangeOrder(model.CompanyId, model.CurrentId, model.ToSwapId);
            return Ok();
        }

        private async Task PublishLesson(PublishLessonModel model)
        {
            var lesson = await _lessonService.GetById(model.CompanyId, model.LessonId);
            if (!lesson.Published)
            {
                lesson.Published = true;
                await _lessonService.AddOrUpdate(lesson);
            }
        }
    }
}
