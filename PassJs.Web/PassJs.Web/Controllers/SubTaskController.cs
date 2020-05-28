﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PassJs.Core.Interfaces;
using PassJs.DomainModels;
using PassJs.Web.Infrastructure.Extensions;
using PassJs.Web.Models;
using PassJs.Web.Models.SubTasks;
using PassJs.Web.Models.TaskHeads;

namespace PassJs.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    public class SubTaskController : ControllerBase
    {
        private readonly ISubTaskService _subTaskService;

        public SubTaskController(ISubTaskService SubTaskService)
        {
            _subTaskService = SubTaskService;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Get(Guid companyId, int id)
        {
            var SubTask = await _subTaskService.GetById(companyId, id);
            return Ok(Mapper.Map<SubTaskModel>(SubTask));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromBody] SubTaskModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var SubTask = await _subTaskService.AddOrUpdate(Mapper.Map<SubTask>(model));
            if (model.PublishNow)
            {
                await PublishSubTask(new PublishSubTaskModel
                {
                    CompanyId = SubTask.CompanyId,
                    TaskHeadId = SubTask.TaskHeadId,
                    SubTaskId = SubTask.Id
                });
            }
            
            model.Order = SubTask.Order;
            model.Id = SubTask.Id;
            return Ok(model);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Publish([FromBody] PublishSubTaskModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await PublishSubTask(model);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Remove([FromBody] RemoveModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());
            await _subTaskService.Remove(model.CompanyId, model.Id);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangeOrder([FromBody] ChangeOrderModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            await _subTaskService.ChangeOrder(model.CompanyId, model.CurrentId, model.ToSwapId);
            return Ok();
        }

        private async Task PublishSubTask(PublishSubTaskModel model)
        {
            var SubTask = await _subTaskService.GetById(model.CompanyId, model.SubTaskId);
            if (!SubTask.Published)
            {
                SubTask.Published = true;
                await _subTaskService.AddOrUpdate(SubTask);
            }
        }
    }
}
