using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CodeSchool.Domain;
using CodeSchool.Web.Models.Lessons;

namespace CodeSchool.Web.Attributes
{
    public class RequiredForLessonTypeAttribute : ValidationAttribute
    {
        private readonly LessonType[] _types;
        public RequiredForLessonTypeAttribute(params LessonType[] types)
        {
            _types = types;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (LessonModel) validationContext.ObjectInstance;
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