using System;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.Web.Models;
using CodeSchool.Web.Models.Lessons;
using Microsoft.AspNetCore.Authorization;
using CodeSchool.Web.Infrastructure;
using CodeSchool.Web.Models.Chapters;

namespace CodeSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IUserLessonService _userLessonService;
        private readonly IUserService _userService;
        private readonly ISimpleCRUDService _simpleCrudService;

        public LessonController(ILessonService lessonService, 
            IUserLessonService userLessonService, 
            IUserService userService, 
            ISimpleCRUDService simpleCrudService)
        {
            _lessonService = lessonService;
            _userLessonService = userLessonService;
            _userService = userService;
            _simpleCrudService = simpleCrudService;
        }

        [HttpGet]
        [Route("[action]/{companyId}/{id}")]
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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ShareLesson([FromBody] ShareLessonModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var newUser = await _userService.CreateNew(new User()
            {
                CompanyId = model.CompanyId,
                Email = model.UserEmail,
                IsAdmin = false,
                UserName = model.UserFullName
            });

            var token = await _simpleCrudService.CreateOrUpdate<Token>(new Token()
            {
                CreatedDt = DateTime.UtcNow,
                LifetimeInDays = model.LinkLifetimeInDays,
                TokenValue = Guid.NewGuid(),
                UserId = newUser.Id
            });

            await _userLessonService.Add(newUser.Id, model.ChapterId, model.LessonId, model.TaskDurationTimeLimitTimeSpan);

            return Ok(token.TokenValue.ToString());
        }

        private async Task PublishLesson(PublishLessonModel model)
        {
            var lesson = await _lessonService.GetById(model.CompanyId, model.LessonId);
            if (!lesson.Published)
            {
                await _userLessonService.Add(model.LessonId, model.ChapterId);
                lesson.Published = true;
                await _lessonService.AddOrUpdate(lesson);
            }
        }
    }
}
