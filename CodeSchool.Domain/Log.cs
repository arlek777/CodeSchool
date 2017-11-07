using System;

namespace CodeSchool.Domain
{
    public enum LogLevel
    {
        Info = 0,
        Warning,
        Error
    }

    public class Log
    {
        public int Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public LogLevel Level { get; set; }
    }
}
