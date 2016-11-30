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
#pragma warning disable 67
        public event Action<bool> AddLogger = delegate { };
#pragma warning restore 67
        public Logger(ILog baseLogger)
        {
            _loggers.Add(baseLogger);
        }
        public async Task LogException(string message, Exception ex, [CallerMemberName] string member = null)
        {
            message += ex.Message;
            if (ex.InnerException != null)
                message += ex.InnerException.Message;
            await Log(message, member, LogSeverity.Exception);
        }
        public async Task LogError(string message, [CallerMemberName] string member = null)
        {
            await Log(message, member, LogSeverity.Error);
        }
        public async Task LogWarning(string message, [CallerMemberName] string member = null)
        {
            await Log(message, member, LogSeverity.Warning);
        }
        public async Task LogInfo(string message, [CallerMemberName] string member = null)
        {
            await Log(message, member, LogSeverity.Information);
        }
        public async Task Suspend()
        {
            foreach (var logger in _loggers)
                await logger.Suspend();
        }
        public Task Resume()
        {
            return Task.FromResult(0);
        }
        #endregion
        #region Private
        private List<ILog> _loggers = new List<ILog>();
        private object _addLock = new object();
        private async Task Log(string message, string method, LogSeverity severity, int code = 0)
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
                await logger.Log(newEntry);
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
