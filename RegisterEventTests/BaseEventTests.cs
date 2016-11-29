using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace EventBase.Tests
{
    [TestClass()]
    public class BaseEventTests
    {
        private Mock<BaseEvent> baseEvent;
        [TestInitialize]
        public void Initialize()
        {
            baseEvent = new Mock<BaseEvent>();
        }
        [TestMethod()]
        public void BaseEventTest()
        {
        }

        [TestMethod()]
        public void NameTest()
        {
            try
            {
                var name = baseEvent.Object.Name();
                Assert.Fail();
            }
            catch
            {
            }
        }

        [TestMethod()]
        public void ProcessTest()
        {
            for(int index=1;index<101;index++)
            {
                var result = baseEvent.Object.Process(index);
                if (Convert.ToInt32(result) != index)
                    Assert.Fail();
            }
        }
    }
}