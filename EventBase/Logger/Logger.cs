using EventBase.Interfaces;
using EventBase.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
// ReSharper disable InconsistentlySynchronizedField

namespace EventBase.Logger
{
    public class Logger : ILogger
    {
        #region Public
        public Logger(ILog baseLogger)
        {
            _loggers.Add(baseLogger);
        }
        public void LogException(string message, Exception ex, [CallerMemberName] string member = null)
        {
            message += ex.Message;
            if (ex.InnerException != null)
                message += ex.InnerException.Message;
            Log(message, member, LogSeverity.Exception);
        }
        public void LogError(string message, [CallerMemberName] string member = null)
        {
            Log(message, member, LogSeverity.Error);
        }
        public void LogWarning(string message, [CallerMemberName] string member = null)
        {
            Log(message, member, LogSeverity.Warning);
        }
        public void LogInfo(string message, [CallerMemberName] string member = null)
        {
            Log(message, member, LogSeverity.Information);
        }
        public void Stop()
        {
            foreach (ILog logger in _loggers)
                logger.Stop();
            _loggers.Clear();
        }
        #endregion
        #region Private
        private List<ILog> _loggers = new List<ILog>();
        private Object _addLock = new object();
        private void Log(string message, string method, LogSeverity severity, int code = 0)
        {
            var newEntry = new LogEntry()
            {
                Message = message,
                Severity = severity,
                Source = method,
                Code = code,
                TimeStamp = DateTime.Now
            };
            foreach (ILog logger in _loggers)
            {
                logger.Log(newEntry);
            }
        }
        public void AddLog(ILog log)
        {
            lock (_addLock)
            {
                _loggers.Add(log);
            }
        }
        public void DelLog(ILog log)
        {
            lock (_addLock)
            {
                _loggers.Remove(log);
            }
        }
        #endregion
    }
}
