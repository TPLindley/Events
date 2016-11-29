using EventBase.Interfaces;
using System;
using System.Configuration;

namespace Events.Common
{
    sealed class Settings : ApplicationSettingsBase, ISettings
    {
        [ApplicationScopedSetting]
        [DefaultSettingValue("RegisterEvent.dll,DiagnoseEvent.dll")]
        public String EventLibraries
        {
            get { return (String)this["EventLibraries"]; }
            set { this["EventLibraries"] = value; }
        }
        public String GetEventLibraries()
        {
            return EventLibraries;
        }
    }
}
