using EventBase.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Events.Common
{
    public class AppSettings
    {
        public List<string> EventLibraries { get; set; }
    }
    sealed class Settings : ISettings
    {
        private ConfigurationBuilder _configBuilder;
        public IConfiguration Configuration { get; set; }
        public AppSettings AppSettings
        {
            get { return Configuration?.Get<AppSettings>(); }
        }
        public Settings()
        {
            try
            {
                var appSettings = new AppSettings() { EventLibraries = new List<string>() };
                _configBuilder = new ConfigurationBuilder();
                _configBuilder.AddJsonFile("EventSettings.json");
                Configuration = _configBuilder.Build();
                var set = AppSettings;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
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
