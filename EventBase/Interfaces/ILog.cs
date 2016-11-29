using EventBase.Models;
using System.Threading.Tasks;

namespace EventBase.Interfaces
{
    public interface ILog
    {
        Task Log(LogEntry logEntry);
        Task Suspend();
        Task Resume();
    }
}
