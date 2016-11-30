using EventBase.Interfaces;
using EventBase.Models;
using System;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;
// ReSharper disable UnusedMember.Local

namespace EventBase.Logger
{
    /// <summary>
    /// Example logging function. Was implemented with WinRT ETW, but would not convert
    /// </summary>
    [EventSource(Name = "EventsLog", Guid= "{fa17d3b0-0c84-488d-9805-4e27a7cdbbc9}")]
    public class EtwLogger : EventSource, ILog
    {
        #region Public
        public EtwLogger() : base(true)
        {
        }
        public void Log(LogEntry logEntry)
        {
            WriteEvent(_eventId++, logEntry.ToString());
        }
        public void Stop()
        {
            Dispose();
        }
        #endregion
        #region Private
        private int _eventId = 1;
        private string GetTimeStamp()
        {
            DateTime now = DateTime.Now;
            return string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                 "{0:D2}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}",
                                 now.Year - 2000,
                                 now.Month,
                                 now.Day,
                                 now.Hour,
                                 now.Minute,
                                 now.Second);
        }


        #endregion
    }
}
