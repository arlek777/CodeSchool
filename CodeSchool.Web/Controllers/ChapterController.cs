using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic;
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
        private readonly ISimpleCRUDService _simpleCrudService;
        private readonly IUserService _userService;
        private readonly IUserChapterService _userChapterService;

        public ChapterController(IChapterService chapterService, 
            ISimpleCRUDService simpleCrudService, 
            IUserService userService, 
            IUserChapterService userChapterService)
        {
            _chapterService = chapterService;
            _simpleCrudService = simpleCrudService;
            _userService = userService;
            _userChapterService = userChapterService;
        }

        [HttpGet]
        [Route("[action]/{companyId}")]
        public async Task<IActionResult> Get(Guid companyId)
        {
            var chapters = await _chapterService.GetChapters(companyId);
            return Ok(chapters.Select(Mapper.Map<ChapterShortcutModel>));
        }

        [HttpGet]
        [Route("[action]/{categoryId}/{companyId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId, Guid companyId)
        {
            var chapters = await _chapterService.GetChapters(companyId, categoryId);
            return Ok(chapters.Select(Mapper.Map<ChapterShortcutModel>));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] ChapterShortcutModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var chapter = await _chapterService.AddOrUpdate(Mapper.Map<Chapter>(model));

            model.Id = chapter.Id;
            model.Order = chapter.Order;
            return Ok(model);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] RemoveModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());
            await _chapterService.Remove(model.CompanyId, model.Id);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangeOrder([FromBody] ChangeOrderModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _chapterService.ChangeOrder(model.CompanyId, model.CurrentId, model.ToSwapId);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ShareChapter([FromBody] ShareChapterModal model)
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

            await _userChapterService.Add(newUser.Id, newUser.CompanyId, model.ChapterId);

            return Ok(token.TokenValue.ToString());
        }
    }
}