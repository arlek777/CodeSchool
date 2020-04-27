using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Infrastructure.Extensions
{
    public static class ApiControllerExtensions
    {
        public static string GetFullUrl(this Controller controller, string relativeUrl)
        {
            var request = controller.HttpContext.Request;
            if (request == null)
            {
                return string.Empty;
            }

            var protocol = request.IsHttps ? "https://" : "http://";
            var port = request.Host.Port.HasValue ? ":" + request.Host.Port : string.Empty;
            var host = request.Host.Host;

            return $"{protocol}{host}{port}{relativeUrl}";
        }
    }
}