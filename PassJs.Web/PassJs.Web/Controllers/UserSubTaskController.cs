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
using PassJs.Web.Models.SubTasks;

namespace PassJs.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    [Route("api/[controller]")]
    public class UserSubTaskController : ControllerBase
    {
        private readonly IUserSubTaskService _userSubTaskService;
        private readonly IUserTaskHeadService _userTaskHeadService;

        public UserSubTaskController(IUserSubTaskService userSubTaskService, IUserTaskHeadService userTaskHeadService)
        {
            _userSubTaskService = userSubTaskService;
            _userTaskHeadService = userTaskHeadService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(int userSubTaskId, Guid userId)
        {
            var userSubTask = await _userSubTaskService.GetUserSubTaskById(userId, userSubTaskId);
            var mappedSubTask = Mapper.Map<UserSubTaskModel>(userSubTask);

            mappedSubTask.SubTask.Answer = string.Empty;
            mappedSubTask.UserId = Guid.Empty;
            mappedSubTask.SubTask.CompanyId = Guid.Empty;
            mappedSubTask.TimeLimit = userSubTask.UserTaskHead.TaskDurationLimit ?? 0;

            return Ok(mappedSubTask);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUserSubTaskIds(int userTaskHeadId , Guid userId)
        {
            var userSubTasks = await _userSubTaskService.GetUserSubTasksById(userId, userTaskHeadId);
            return Ok(userSubTasks.Select(l => new { id = l.Id, isPassed = l.IsPassed }));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateUserSubTaskModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _userSubTaskService.Update(new UserSubTask()
            {
                Id = model.Id,
                Code = model.Code,
                SelectedAnswerOptionId = model.SelectedAnswerOptionId,
                Score = model.Score,
                IsPassed = model.IsPassed,
                UserId = model.UserId,
                UserTaskHeadId = model.UserTaskHeadId
            });
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AutoSave([FromBody] UserTaskSnapshot model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());
            await _userSubTaskService.SaveSnapshot(new PassJs.Core.Models.UserTaskSnapshot()
            {
                Id = model.UserSubTask.Id,
                Code = model.UserSubTask.Code,
                UserId = model.UserSubTask.UserId,
                UserTaskHeadId = model.UserSubTask.UserTaskHeadId,
                CopyPasteCount = model.CPC,
                UnfocusCount = model.UF
            });
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CanOpen([FromBody] CanOpenSubTaskModel model)
        {
            var canOpenTaskHead = await _userTaskHeadService.CanOpen(model.UserId, model.UserTaskHeadId);
            return Ok(canOpenTaskHead);
            if (canOpenTaskHead)
            {
                var canOpenSubTask = await _userSubTaskService.CanOpen(model.UserId, model.UserTaskHeadId, model.UserSubTaskId);
                return Ok(canOpenSubTask);
            }

            return Ok(false);
        }
    }
}