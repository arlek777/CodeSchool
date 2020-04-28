using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LogController: Controller
    {
        private readonly IHostingEnvironment _env;

        public LogController(IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult Get()
        {
            return File(System.IO.File.ReadAllBytes(_env.ContentRootPath + "/logs.txt"), "application/json");
        }
    }
}
