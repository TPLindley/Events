using System;
using EventBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable EmptyGeneralCatchClause

namespace RegisterEventTests
{
    [TestClass()]
    public class BaseEventTests
    {
        //        private Mock<BaseEvent> baseEvent;
        private MyEvent _baseEvent;
        public class MyEvent : BaseEvent
        {
        }
        [TestInitialize]
        public void Initialize()
        {
            //            baseEvent = new Mock<BaseEvent>();
            _baseEvent = new MyEvent();
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
                _baseEvent?.Name();
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
                var result = _baseEvent.Process(index);
                if (Convert.ToInt32(result) != index)
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void ProcessTestNegativeNumber()
        {
            try
            {
                _baseEvent?.Process(-1);
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
                var output = _baseEvent.Process(101);
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