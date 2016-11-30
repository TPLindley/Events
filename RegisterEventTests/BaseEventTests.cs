using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics;

namespace EventBase.Tests
{
    [TestClass()]
    public class BaseEventTests
    {
        //        private Mock<BaseEvent> baseEvent;
        private myEvent baseEvent;
        public class myEvent : BaseEvent
        {
        }
        [TestInitialize]
        public void Initialize()
        {
            //            baseEvent = new Mock<BaseEvent>();
            baseEvent = new myEvent();
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
                var name = baseEvent.Name();
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
                var result = baseEvent.Process(index);
                if (Convert.ToInt32(result) != index)
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void ProcessTestNegativeNumber()
        {
            try
            {
                var output = baseEvent.Process(-1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (ex.GetType() != typeof(ArgumentException))
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void ProcessTestInvalidPositiveNumber()
        {
            try
            {
                var output = baseEvent.Process(101);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (ex.GetType() != typeof(ArgumentException))
                    Assert.Fail();
            }
        }
    }
}