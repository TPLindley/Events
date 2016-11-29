using System;

namespace EventBase.Models
{
    public enum LogSeverity { Information, Warning, Error, Exception }
    public class LogEntry
    {
        public DateTime TimeStamp { get; set; }
        public LogSeverity Severity { get; set; }
        public int Code { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
    }
}
