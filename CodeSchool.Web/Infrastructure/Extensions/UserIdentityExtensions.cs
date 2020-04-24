using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace CodeSchool.Web.Infrastructure.Extensions
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
    }
}