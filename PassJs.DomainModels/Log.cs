using System;

namespace PassJs.DomainModels
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
        public DateTime TimeStamp { get; set; }
        public LogLevel Level { get; set; }
    }
}
