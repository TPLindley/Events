using System.Collections.Generic;

namespace EventBase.Interfaces
{
    /// <summary>
    /// Interface definition for application settings
    /// </summary>
    public interface ISettings
    {
        List<string> GetEventLibraries();
    }
}
