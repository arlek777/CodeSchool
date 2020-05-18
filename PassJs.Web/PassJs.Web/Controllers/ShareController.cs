using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PassJs.Core;
using PassJs.Core.Interfaces;
using PassJs.DomainModels;
using PassJs.Web.Infrastructure.Extensions;
using PassJs.Web.Models.TaskHeads;

namespace PassJs.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    public class ShareController : ControllerBase
    {
        private readonly ISimpleCRUDService _simpleCrudService;
        private readonly IUserService _userService;
        private readonly IUserTaskHeadService _userTaskHeadService;
        private readonly IUserSubTaskService _userSubTaskService;

        public ShareController(
            ISimpleCRUDService simpleCrudService,
            IUserService userService, 
            IUserTaskHeadService userTaskHeadService, 
            IUserSubTaskService userSubTaskService)
        {
            _simpleCrudService = simpleCrudService;
            _userService = userService;
            _userTaskHeadService = userTaskHeadService;
            _userSubTaskService = userSubTaskService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ShareTaskHead([FromBody] ShareTaskHeadModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var token = await CreateUserWithToken(model);
            await _userTaskHeadService.AddTaskHeadSubTasks(token.UserId, model.CompanyId, model.TaskHeadId, model.ParsedTaskDurationTimeLimitTime);

            return Ok(GenerateShareLink(token.TokenValue));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ShareSubTask([FromBody] ShareSubTaskModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var token = await CreateUserWithToken(model);
            await _userSubTaskService.Add(token.UserId, model.SubTaskId, model.TaskHeadId, model.ParsedTaskDurationTimeLimitTime);

            return Ok(GenerateShareLink(token.TokenValue));
        }

        private string GenerateShareLink(Guid tokenValue)
        {
            var base64Token = Base64UrlEncoder.Encode(tokenValue.ToString());
            var encodedToken = WebUtility.UrlEncode(base64Token);

            var result = this.GetFullUrl($"/invitation/{encodedToken}");
            return result;
        }

        private async Task<Token> CreateUserWithToken(ShareTaskHeadModel model)
        {
            var newUser = await _userService.CreateNew(new PassJs.DomainModels.User()
            {
                CompanyId = model.CompanyId,
                CompanyName = User.GetCompanyName(),
                Email = model.UserEmail,
                IsAdmin = false,
                UserName = model.UserFullName
            });

            var token = await _simpleCrudService.CreateOrUpdate<Token>(new Token()
            {
                CreatedDt = DateTime.UtcNow,
                LifetimeInDays = model.LinkLifetimeInDays,
                TokenValue = Guid.NewGuid(),
                UserId = newUser.Id,
                ExtraData = model.TaskDurationTimeLimit
            });

            return token;
        }
    }
}