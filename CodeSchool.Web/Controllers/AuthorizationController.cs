using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Services;
using CodeSchool.Domain;
using CodeSchool.Web.Infrastructure;
using CodeSchool.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
    [Route("api/auth")]
    public class AuthorizationController : Controller
    {
        private readonly IUserService _userService;
        private readonly JWTTokenProvider _jwtTokenProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserLessonService _userLessonService;

        public AuthorizationController(IUserService userService, 
            JWTTokenProvider jwtTokenProvider, 
            IPasswordHasher passwordHasher,
            IUserLessonService userLessonService)
        {
            _userService = userService;
            _jwtTokenProvider = jwtTokenProvider;
            _passwordHasher = passwordHasher;
            _userLessonService = userLessonService;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            var user = await _userService.GetByEmail(loginModel.Email);
            if (user == null)
            {
                return BadRequest(ValidationResultMessages.LoginWrongCredentials);
            }

            var result = _passwordHasher.VerifyHashedPassword(user.Password, loginModel.Password);
            if (result == PasswordVerificationResult.Failed)
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
            var existingUser = await _userService.GetByEmail(model.Email);
            if (existingUser != null) return BadRequest(ValidationResultMessages.DuplicateEmail);

            var user = new User()
            {
                Email = model.Email,
                UserName = model.UserName,
                Password = _passwordHasher.HashPassword(model.Password)
            };

            user = await _userService.CreateNew(user);
            await _userLessonService.CreateForNewUser(user.Id);

            var tokens = GetJWTTokens(user);
            return Ok(tokens);
        }

        private dynamic GetJWTTokens(User user)
        {
            return new
            {
                accessToken = _jwtTokenProvider.GetAccessToken(user),
                idToken = _jwtTokenProvider.GetIdToken(user)
            };
        }
    }
}