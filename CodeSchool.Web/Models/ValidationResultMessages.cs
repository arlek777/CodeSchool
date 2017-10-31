namespace CodeSchool.Web.Models
{
    public static class ValidationResultMessages
    {
        public const string LoginWrongCredentials = "loginWrongCredentials";
        public const string UserNameRequiredOrInvalid = "userNameRequiredOrInvalid";

        public const string EmailRequiredOrInvalid = "emailRequiredOrInvalid";
        public const string DuplicateEmail = "duplicateEmail";

        public const string PasswordShort = "passwordShort";
        public const string PasswordInvalidFormat = "passwordInvalidFormat";
    }
}
