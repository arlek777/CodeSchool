using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PassJs.Core.Interfaces;
using PassJs.DomainModels;
using PassJs.Web.Infrastructure.Extensions;
using PassJs.Web.Models;
using PassJs.Web.Models.TaskHeads;

namespace PassJs.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    public class TaskHeadController : ControllerBase
    {
        private readonly ITaskHeadService _taskHeadService;
    
        public TaskHeadController(ITaskHeadService TaskHeadService)
        {
            _taskHeadService = TaskHeadService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get(Guid companyId)
        {
            var TaskHeads = await _taskHeadService.GetTaskHeads(companyId);
            return Ok(TaskHeads.Select(Mapper.Map<TaskHeadShortcutModel>));
        }

        [HttpGet]
        [Route("[action]/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId, Guid companyId)
        {
            var TaskHeads = await _taskHeadService.GetTaskHeads(companyId, categoryId);
            return Ok(TaskHeads.Select(Mapper.Map<TaskHeadShortcutModel>));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] TaskHeadShortcutModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var TaskHead = await _taskHeadService.AddOrUpdate(Mapper.Map<TaskHead>(model));

            model.Id = TaskHead.Id;
            model.Order = TaskHead.Order;
            return Ok(model);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] RemoveModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());
            await _taskHeadService.Remove(model.CompanyId, model.Id);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangeOrder([FromBody] ChangeOrderModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _taskHeadService.ChangeOrder(model.CompanyId, model.CurrentId, model.ToSwapId);
            return Ok();
        }
    }
}