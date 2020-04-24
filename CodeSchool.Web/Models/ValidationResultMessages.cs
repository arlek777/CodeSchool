namespace CodeSchool.Web.Models
{
    public static class ValidationResultMessages
    {
        public const string LoginWrongCredentials = "Пользователь с таким e-mail и паролем не найден.";

        public const string EmailRequiredOrInvalid = "E-mail имеет не верный формат.";
        public const string DuplicateEmail = "Пользователь с таким e-mail уже зарегистрирован.";

        public const string PasswordShort = "Минимальный пароль 5 символов.";
        public const string PasswordInvalidFormat = "Пароль имеет не верный формат.";

        public const string MaxLength = "Максимальная длина {0} {1} была достигнута.";

        public const string UnhandledError = "Упс:( Возникла не известная ошибка.";

        public const string RequiredField = "Заполните {0}.";
    }
}
