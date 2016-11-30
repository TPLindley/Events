using System;

namespace EventBase.Models
{
    public enum LogSeverity { Information, Warning, Error, Exception }
    /// <summary>
    /// Property bag for event logging
    /// </summary>
    public class LogEntry
    {
        public DateTime TimeStamp { get; set; }
        public LogSeverity Severity { get; set; }
        public int Code { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return $"{TimeStamp.ToShortDateString()}:{Severity} {Code}-{Source}-{Message}";
        }
    }
}
