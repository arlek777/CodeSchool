using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace PassJs.Web.Infrastructure.Extensions
{
    public static class UserIdentityExtensions
    {
        public static Guid GetCompanyId(this ClaimsPrincipal identity)
        {
            var companyIdStr = identity.Claims.FirstOrDefault(c => c.Type.Contains("companyId"));
            return Guid.Parse(companyIdStr.Value);
        }

        public static string GetCompanyName(this ClaimsPrincipal identity)
        {
            var companyNameStr = identity.Claims.FirstOrDefault(c => c.Type.Contains("companyName"));
            return companyNameStr?.Value;
        }

        public static Guid GetUserId(this ClaimsPrincipal identity)
        {
            var userIdStr = identity.Claims.FirstOrDefault(c => c.Type.Contains(ClaimTypes.NameIdentifier));
            return Guid.Parse(userIdStr.Value);
        }
    }
}