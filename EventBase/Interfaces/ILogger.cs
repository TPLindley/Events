using System;
using System.Threading.Tasks;

namespace EventBase.Interfaces
{
    /// <summary>
    /// Interface definition for event logging service
    /// </summary>
    public interface ILogger
    {
        void LogInfo(string message, string member = null);
        void LogWarning(string message, string member = null);
        void LogError(string message, string member = null);
        void LogException(string message, Exception ex, string member = null);
        void AddLog(ILog log);
        void DelLog(ILog log);
        void Stop();
    }
}
