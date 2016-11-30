using EventBase.Interfaces;
using EventBase.Models;
using System.Diagnostics.Tracing;

namespace EventBase.Logger
{
    /// <summary>
    /// Example logging function. Was implemented with WinRT ETW, but would not convert
    /// </summary>
    [EventSource(Name = "EventsLog", Guid= "{eeeeef10-0ffd-4b70-b8c6-1977f436b72e}")]
    public class EtwLogger : EventSource, ILog
    {
        #region Public
        public EtwLogger() : base(true)
        {
        }
        public void Log(LogEntry logEntry)
        {
            Write(logEntry.ToString());
        }
        public void Stop()
        {
            Dispose();
        }
        #endregion
    }
}
