using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PassJs.Core.Interfaces;
using PassJs.DomainModels;
using PassJs.Web.Helpers;
using PassJs.Web.Infrastructure.Extensions;
using PassJs.Web.Infrastructure.Services;
using PassJs.Web.Models;

namespace PassJs.Web.Controllers
{
    [Route("api/auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JWTTokenProvider _jwtTokenProvider;

        public AuthorizationController(IUserService userService, 
            JWTTokenProvider jwtTokenProvider)
        {
            _userService = userService;
            _jwtTokenProvider = jwtTokenProvider;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var user = await _userService.GetByEmail(loginModel.Email);
            if (user == null)
            {
                return BadRequest(ValidationResultMessages.LoginWrongCredentials);
            }

            var result = PasswordHashHelper.VerifyHashedPassword(user.Password, loginModel.Password);
            if (!result || !user.IsAdmin)
            {
                return BadRequest(ValidationResultMessages.LoginWrongCredentials);
            }

            var tokens = GetJWTTokens(user);
            return Ok(tokens);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegistrationModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetFirstError());

            var existingUser = await _userService.GetByEmail(model.Email);
            if (existingUser != null) return BadRequest(ValidationResultMessages.DuplicateEmail);

            var user = new User()
            {
                Email = model.Email,
                UserName = model.UserName,
                Password = PasswordHashHelper.HashPassword(model.Password),
                IsAdmin = true,
            };

            if (!string.IsNullOrWhiteSpace(model.CompanyName))
            {
                user.CompanyId = Guid.NewGuid();
                user.CompanyName = model.CompanyName;
            }

            user = await _userService.CreateNew(user);
            var tokens = GetJWTTokens(user);

            return Ok(tokens);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> LoginByToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest();
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return BadRequest("You have already been authenticated.");
            }

            var parsedToken = GetParsedToken(token);

            var dbToken = await _userService.GetUserToken(parsedToken);
            if (dbToken == null)
            {
                return BadRequest(ValidationResultMessages.LoginWrongCredentials);
            }

            var tokens = GetJWTTokens(dbToken.User);

            await _userService.RemoveUserToken(dbToken);

            return Ok(tokens);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetInvitation(string token)
        {
            if (HttpContext.User.Identity.IsAuthenticated || string.IsNullOrWhiteSpace(token))
            {
                return Ok(false);
            }

            var parsedToken = GetParsedToken(token);
            var dbToken = await _userService.GetUserToken(parsedToken);

            if (dbToken?.User == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                from = dbToken.User.CompanyName,
                timeLimit = !string.IsNullOrWhiteSpace(dbToken.ExtraData) ? dbToken.ExtraData : null
            });
        }

        private Guid GetParsedToken(string token)
        {
            var base64Token = Base64UrlEncoder.Decode(token);
            var decodedToken = WebUtility.UrlDecode(base64Token);
            var parsedToken = Guid.Parse(decodedToken);
            return parsedToken;
        }

        private dynamic GetJWTTokens(User user, bool sessionLifetime = false)
        {
            return new
            {
                accessToken = _jwtTokenProvider.GetAccessToken(user, sessionLifetime),
                idToken = _jwtTokenProvider.GetIdToken(user, sessionLifetime)
            };
        }
    }
}