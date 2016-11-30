using EventBase.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Events.Common.Tests
{
    [TestClass()]
    public class SettingsTests
    {
        private ISettings _settings;
        [TestInitialize()]
        public void Initialize()
        {
            _settings = new Settings();
        }
        [TestMethod()]
        public void GetEventLibrariesTest()
        {
            var libraries = _settings.GetEventLibraries();
            if (libraries.Count < 1)
                Assert.Fail();
            if (!libraries.First().StartsWith("Register"))
                Assert.Fail();
            if (libraries.FirstOrDefault(l=>l.StartsWith("DiagnoseEvent"))==null)
                Assert.Fail();
        }
    }
}