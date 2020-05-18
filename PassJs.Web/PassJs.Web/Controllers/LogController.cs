using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace PassJs.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class LogController: ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public LogController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Get()
        {
            return File(System.IO.File.ReadAllBytes(_env.ContentRootPath + "/logs.txt"), "application/json");
        }
    }
}
