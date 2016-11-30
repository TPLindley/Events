using EventBase.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Events.Common
{
    /// <summary>
    /// AppSettings class will hold all the settings for the application.
    /// 
    /// Currently, this is only a list of the event handler libraries to load
    /// 
    /// </summary>
    public class AppSettings
    {
        public List<string> EventLibraries { get; set; }
    }
    /// <summary>
    /// Settings class to handle reading/writing settings to the application settings file
    /// 
    /// Note: This approach will work with both WPF and Modern Applications
    /// 
    /// </summary>
    public class Settings : ISettings
    {
        private readonly ConfigurationBuilder _configBuilder;
        public IConfiguration Configuration { get; set; }
        public AppSettings AppSettings => Configuration?.Get<AppSettings>();

        public Settings()
        {
            try
            {
                _configBuilder = new ConfigurationBuilder();
                _configBuilder.AddJsonFile("EventSettings.json");
                Configuration = _configBuilder.Build();
            }
            catch (Exception ex)
            {
                var tokens = ex.Message.Split('\'');
                var settings = new AppSettings()
                {
                    EventLibraries = new List<string>()
                {
                    "RegisterEvent,Version=1.0.0.0,Culture=neutral,PublicKeyToken=null",
                    "DiagnoseEvent,Version=1.0.0.0,Culture=neutral,PublicKeyToken=null"
                }
                };
                using (StreamWriter file = File.CreateText(tokens[3]))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, settings);
                }
                Configuration = _configBuilder.Build();
            }
        }
        public List<string> GetEventLibraries()
        {
            return AppSettings.EventLibraries;
        }
    }
}
