using System;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using PassJs.Web.Infrastructure.Extensions;

namespace PassJs.Web.Attributes
{
    public class AutoSetUserInfoFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            AutoSetUserInfo(context);
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            AutoSetUserInfo(context);
            return base.OnActionExecutionAsync(context, next);
        }

        private void AutoSetUserInfo(ActionExecutingContext context)
        {
            if (context.HttpContext.User != null && context.HttpContext.User.Identity.IsAuthenticated)
            {
                var companyId = context.HttpContext.User.Identity.GetCompanyId();
                var userId = context.HttpContext.User.Identity.GetSubjectId();

                try
                {
                    if (context.ActionArguments.ContainsKey("companyId")) context.ActionArguments.Remove("companyId");
                    if (context.ActionArguments.ContainsKey("userId")) context.ActionArguments.Remove("userId");

                    context.ActionArguments.Add("companyId", companyId);
                    context.ActionArguments.Add("userId", Guid.Parse(userId));

                    context.ActionArguments.TryGetValue("model", out var model);
                    if (model != null)
                    {
                        var companyIdProp = model.GetType().GetProperty("CompanyId", typeof(Guid));
                        companyIdProp?.SetValue(model, companyId, null);

                        var userIdProp = model.GetType().GetProperty("UserId", typeof(Guid));
                        userIdProp?.SetValue(model, Guid.Parse(userId), null);
                    }
                }
                catch (Exception e)
                {
                    var msg = e.Message;
                }
                
            }
        }
    }
}