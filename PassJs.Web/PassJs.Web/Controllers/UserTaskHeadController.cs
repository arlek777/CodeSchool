using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PassJs.Web.Models.TaskHeads;

namespace PassJs.Web.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    public class UserTaskHeadController : ControllerBase
    {
        private readonly IUserTaskHeadService _TaskHeadService;

        public UserTaskHeadController(IUserTaskHeadService TaskHeadService)
        {
            _TaskHeadService = TaskHeadService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get(FilterUserTaskHeadModel model)
        {
            var TaskHeads = await _TaskHeadService.GetUserTaskHeads(model);
            return Ok(TaskHeads.Select(Mapper.Map<UserTaskHeadShortcutModel>));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CanOpen([FromBody] CanOpenTaskHeadModel model)
        {
            return Ok(await _TaskHeadService.CanOpen(model.UserId, model.UserTaskHeadId));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetFirstTaskHeadAndSubTask(Guid userId)
        {
            var result = await _TaskHeadService.GetFirstTaskHeadAndSubTask(userId);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> StartUserTask(Guid userId)
        {
            await _TaskHeadService.StartUserTask(userId);
            return Ok(true);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> FinishUserTask(Guid userId)
        {
            await _TaskHeadService.FinishUserTask(userId);
            return Ok(true);
        }
    }
}