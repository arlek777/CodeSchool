using System.Threading.Tasks;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.BusinessLogic.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CodeSchool.Web.Controllers
{
    [Route("api/[controller]")]
    public class ErrorController : Controller
    {
        private readonly ILogService _logService;
        private readonly IHostingEnvironment _env;

        public ErrorController(ILogService logService, IHostingEnvironment env)
        {
            _logService = logService;
            _env = env;
        }

        [Route("500")]
        public async Task Handle500()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exceptionMessage = exception.Error.Message;
            var innerExceptionMessage = exception.Error.InnerException?.Message;
            var stackTrace = exception.Error.StackTrace;

            var log = new Log()
            {
                ExceptionMessage = exceptionMessage,
                InnerExceptionMessage = innerExceptionMessage,
                Level = LogLevel.Error,
                StackTrace = stackTrace
            };

            object result;
            if (_env.IsDevelopment())
            {
                result = log;
            }
            else
            {
                await _logService.Log(log);
                result = new { error = "Упс :( Произошла неизвестная ошибка." };
            }

            await HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}