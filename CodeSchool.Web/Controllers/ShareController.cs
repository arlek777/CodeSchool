using System;
using System.Net;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using CodeSchool.Web.Infrastructure.Extensions;
using CodeSchool.Web.Models.Chapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CodeSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class ShareController : Controller
    {
        private readonly ISimpleCRUDService _simpleCrudService;
        private readonly IUserService _userService;
        private readonly IUserChapterService _userChapterService;
        private readonly IUserLessonService _userLessonService;

        public ShareController(
            ISimpleCRUDService simpleCrudService,
            IUserService userService, 
            IUserChapterService userChapterService, 
            IUserLessonService userLessonService)
        {
            _simpleCrudService = simpleCrudService;
            _userService = userService;
            _userChapterService = userChapterService;
            _userLessonService = userLessonService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ShareChapter([FromBody] ShareModal model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var token = await CreateUserWithToken(model);
            await _userChapterService.AddChapterLessons(token.UserId, model.CompanyId, model.ChapterId);

            return Ok(GenerateShareLink(token.TokenValue));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ShareLesson([FromBody] ShareLessonModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var token = await CreateUserWithToken(model);
            await _userLessonService.Add(token.UserId, model.LessonId, model.ChapterId, model.TaskDurationTimeLimitTimeSpan);

            return Ok(GenerateShareLink(token.TokenValue));
        }

        private string GenerateShareLink(Guid tokenValue)
        {
            var base64Token = Base64UrlEncoder.Encode(tokenValue.ToString());
            var encodedToken = WebUtility.UrlEncode(base64Token);

            var result = this.GetFullUrl($"/invitation/{encodedToken}");
            return result;
        }

        private async Task<Token> CreateUserWithToken(ShareModal model)
        {
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

            return token;
        }
    }
}