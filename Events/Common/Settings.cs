using EventBase.Interfaces;
using System;
using System.Configuration;

namespace Events.Common
{
    sealed class Settings : ApplicationSettingsBase, ISettings
    {
        [ApplicationScopedSetting]
        [DefaultSettingValue("RegisterEvent,Version=1.0.0.0,Culture=neutral,PublicKeyToken=null;DiagnoseEvent,Version=1.0.0.0,Culture=neutral,PublicKeyToken=null")]
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
