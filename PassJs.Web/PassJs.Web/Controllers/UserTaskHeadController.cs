using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PassJs.Core.Interfaces;
using PassJs.Core.Models;
using PassJs.Web.Models.TaskHeads;

namespace PassJs.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    [Route("api/[controller]")]
    public class UserTaskHeadController : ControllerBase
    {
        private readonly IUserTaskHeadService _taskHeadService;

        public UserTaskHeadController(IUserTaskHeadService TaskHeadService)
        {
            _taskHeadService = TaskHeadService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get(FilterUserTaskHeadModel model)
        {
            var TaskHeads = await _taskHeadService.GetUserTaskHeads(model);
            return Ok(TaskHeads.Select(Mapper.Map<UserTaskHeadShortcutModel>));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CanOpen([FromBody] CanOpenTaskHeadModel model)
        {
            return Ok(await _taskHeadService.CanOpen(model.UserId, model.UserTaskHeadId));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetFirstTaskHeadAndSubTask(Guid userId)
        {
            var result = await _taskHeadService.GetFirstTaskHeadAndSubTask(userId);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> StartUserTask(Guid userId)
        {
            await _taskHeadService.StartUserTask(userId);
            return Ok(true);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> FinishUserTask(Guid userId)
        {
            await _taskHeadService.FinishUserTask(userId);
            return Ok(true);
        }
    }
}