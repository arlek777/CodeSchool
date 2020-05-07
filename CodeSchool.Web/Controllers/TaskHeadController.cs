using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using CodeSchool.Web.Models.TaskHeads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.Web.Infrastructure.Extensions;

namespace CodeSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class TaskHeadController : Controller
    {
        private readonly ITaskHeadService _TaskHeadService;
    
        public TaskHeadController(ITaskHeadService TaskHeadService)
        {
            _TaskHeadService = TaskHeadService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get(Guid companyId)
        {
            var TaskHeads = await _TaskHeadService.GetTaskHeads(companyId);
            return Ok(TaskHeads.Select(Mapper.Map<TaskHeadShortcutModel>));
        }

        [HttpGet]
        [Route("[action]/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId, Guid companyId)
        {
            var TaskHeads = await _TaskHeadService.GetTaskHeads(companyId, categoryId);
            return Ok(TaskHeads.Select(Mapper.Map<TaskHeadShortcutModel>));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] TaskHeadShortcutModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var TaskHead = await _TaskHeadService.AddOrUpdate(Mapper.Map<TaskHead>(model));

            model.Id = TaskHead.Id;
            model.Order = TaskHead.Order;
            return Ok(model);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] RemoveModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());
            await _TaskHeadService.Remove(model.CompanyId, model.Id);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangeOrder([FromBody] ChangeOrderModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _TaskHeadService.ChangeOrder(model.CompanyId, model.CurrentId, model.ToSwapId);
            return Ok();
        }
    }
}