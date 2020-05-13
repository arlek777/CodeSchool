using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace PassJs.Web.Infrastructure.Extensions
{
    public static class UserIdentityExtensions
    {
        public static Guid GetCompanyId(this IIdentity identity)
        {
            if (identity is ClaimsIdentity claimsPrincipal)
            {
                var companyIdStr = claimsPrincipal.Claims.FirstOrDefault(c => c.Type.Contains("companyId"));
                return Guid.Parse(companyIdStr.Value);
            }

            return Guid.Empty;
        }

        public static string GetCompanyName(this IIdentity identity)
        {
            if (identity is ClaimsIdentity claimsPrincipal)
            {
                var companyNameStr = claimsPrincipal.Claims.FirstOrDefault(c => c.Type.Contains("companyName"));
                return companyNameStr?.Value;
            }

            return null;
        }
    }
}