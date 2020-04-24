using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CodeSchool.Web.Infrastructure.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static string GetFirstError(this ModelStateDictionary modelState)
        {
            var firstError = modelState.Select(ms => ms.Value.Errors.FirstOrDefault()?.ErrorMessage).FirstOrDefault();
            return firstError;
        }
    }
}
