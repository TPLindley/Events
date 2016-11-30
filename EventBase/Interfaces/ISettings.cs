using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBase.Interfaces
{
    public interface ISettings
    {
        List<string> GetEventLibraries();
    }
}
