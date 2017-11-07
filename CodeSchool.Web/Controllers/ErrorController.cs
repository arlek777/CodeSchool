using System.Threading.Tasks;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.BusinessLogic.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

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

        public async Task Index()
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            HttpContext.Response.ContentType = "text/html";
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (ex != null)
            {
                var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
                await HttpContext.Response.WriteAsync(err).ConfigureAwait(false);
            }

            //var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            //var exceptionMessage = exception.Error.Message;
            //var innerExceptionMessage = exception.Error.InnerException?.Message;
            //var stackTrace = exception.Error.StackTrace;

            //var log = new Log()
            //{
            //    ExceptionMessage = exceptionMessage,
            //    InnerExceptionMessage = innerExceptionMessage,
            //    Level = LogLevel.Error,
            //    StackTrace = stackTrace
            //};

            //object result;
            //if (_env.IsDevelopment())
            //{
            //    result = log;
            //}
            //else
            //{
            //    await _logService.Log(log);
            //    result = new { error = "Упс :( Произошла неизвестная ошибка." };
            //}

            //HttpContext.Response.StatusCode = 500;
            //await HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));

            //return Ok();
        }
    }
}