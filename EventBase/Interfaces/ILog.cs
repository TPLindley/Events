using EventBase.Models;
using System.Threading.Tasks;

namespace EventBase.Interfaces
{
    /// <summary>
    /// Interface definition for a specific logging interface, i.e. file, database, memory, etc.
    /// </summary>
    public interface ILog
    {
        void Log(LogEntry logEntry);
        void Stop();
    }
}
