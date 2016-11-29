using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBase.Interfaces
{
    public interface ILogger
    {
        Task LogInfo(string message, string member = null);
        Task LogWarning(string message, string member = null);
        Task LogError(string message, string member = null);
        Task LogException(string message, Exception ex, string member = null);
        void AddLog(ILog log);
        void DelLog(ILog log);
        Task Suspend();
        Task Resume();
    }
}
