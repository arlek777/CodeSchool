using System;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using CodeSchool.Web.Infrastructure;
using CodeSchool.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeSchool.Web.Attributes
{
    public class ApiExceptionFilter: ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogService _logService;

        public ApiExceptionFilter(IHostingEnvironment env, ILogService logService)
        {
            _env = env;
            _logService = logService;
        }

        public override void OnException(ExceptionContext context)
        {
            object apiError;
            var exception = context.Exception;
            if (_env.IsDevelopment())
            {
                apiError = new
                {
                    message = exception.Message,
                    innerException = exception.InnerException?.Message,
                    stackTrace = exception.StackTrace
                };
            }
            else
            {
                apiError = new { message = ValidationResultMessages.UnhandledError };
            }

            _logService.Log(new Log()
            {
                ExceptionMessage = exception.Message,
                InnerExceptionMessage = exception.InnerException?.Message,
                Level = LogLevel.Error,
                StackTrace = exception.StackTrace,
                TimeStamp = DateTime.UtcNow
            });

            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(apiError);

            base.OnException(context);
        }
    }
}
