using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using CodeSchool.Web.Models.Chapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.Web.Infrastructure.Extensions;

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
        public async Task<IActionResult> Get(Guid companyId)
        {
            var chapters = await _chapterService.GetChapters(companyId);
            return Ok(chapters.Select(Mapper.Map<ChapterShortcutModel>));
        }

        [HttpGet]
        [Route("[action]/{categoryId}")]
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
    }
}