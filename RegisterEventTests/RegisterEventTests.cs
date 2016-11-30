using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RegisterEvent.Tests
{
    [TestClass()]
    public class RegisterEventTests
    {
        private Register registerEvent;
        [TestInitialize]
        public void Initialize()
        {
            registerEvent = new Register();
        }
        [TestMethod]
        public void NameTest()
        {
            if (registerEvent.Name().CompareTo(EventLibConstants.Name) != 0)
                Assert.Fail();
        }
        [TestMethod()]
        public void ProcessTest()
        {
            for(var index=1;index<101;index++)
            {
                var output = registerEvent.Process(index);
                if (index % 3 == 0 && index % 5 == 0)
                {
                    if (output.CompareTo($"{EventLibConstants.Register}{EventLibConstants.Patient}") != 0)
                        Assert.Fail();
                }
                else
                {
                    if (index % 3 == 0)
                    {
                        if (output.CompareTo(EventLibConstants.Register.Trim()) != 0)
                            Assert.Fail();
                    }
                    else if (index % 5 == 0)
                    {
                        if (output.CompareTo(EventLibConstants.Patient.Trim()) != 0)
                            Assert.Fail();
                    }
                    else
                    {
                        if (output.CompareTo($"{index}") != 0)
                            Assert.Fail();
                    }
                }
            }
        }
        [TestMethod]
        public void ProcessTestNegativeNumber()
        {
            try
            {
                var output = registerEvent.Process(-1);
                Assert.Fail();
            }
            catch(Exception ex)
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
                var output = registerEvent.Process(101);
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