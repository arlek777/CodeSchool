using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CodeSchool.Domain;
using PassJs.Web.Models.SubTasks;

namespace PassJs.Web.Attributes
{
    public class RequiredForSubTaskTypeAttribute : ValidationAttribute
    {
        private readonly SubTaskType[] _types;
        public RequiredForSubTaskTypeAttribute(params SubTaskType[] types)
        {
            _types = types;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (SubTaskModel) validationContext.ObjectInstance;
            if(_types.Contains(model.Type))
            {
                string GetFormattedError () => String.Format(ErrorMessage, validationContext.MemberName);

                if (value == null)
                {
                    return new ValidationResult(GetFormattedError());
                }

                var isStringTypeAndEmpty = (value as string) != null && String.IsNullOrEmpty((string) value);
                if (isStringTypeAndEmpty)
                {
                    return new ValidationResult(GetFormattedError());
                }

                if (value is Array array && array.Length == 0)
                {
                    return new ValidationResult(GetFormattedError());
                }
            }

            return ValidationResult.Success;
        }
    }
}