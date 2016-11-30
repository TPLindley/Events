using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegisterEvent;

namespace RegisterEventTests
{
    [TestClass()]
    public class RegisterEventTests
    {
        private Register _registerEvent;
        [TestInitialize()]
        public void Initialize()
        {
            _registerEvent = new Register();
        }
        [TestMethod()]
        public void NameTest()
        {
            if (String.Compare(_registerEvent.Name(), EventLibConstants.Name, StringComparison.Ordinal) != 0)
                Assert.Fail();
        }
        [TestMethod()]
        public void ProcessTest()
        {
            for(var index=1;index<101;index++)
            {
                var output = _registerEvent.Process(index);
                if (index % 3 == 0 && index % 5 == 0)
                {
                    if (String.Compare(output, $"{EventLibConstants.Register}{EventLibConstants.Patient}", StringComparison.Ordinal) != 0)
                        Assert.Fail();
                }
                else
                {
                    if (index % 3 == 0)
                    {
                        if (String.Compare(output, EventLibConstants.Register.Trim(), StringComparison.Ordinal) != 0)
                            Assert.Fail();
                    }
                    else if (index % 5 == 0)
                    {
                        if (String.Compare(output, EventLibConstants.Patient.Trim(), StringComparison.Ordinal) != 0)
                            Assert.Fail();
                    }
                    else
                    {
                        if (String.Compare(output, $"{index}", StringComparison.Ordinal) != 0)
                            Assert.Fail();
                    }
                }
            }
        }
        [TestMethod()]
        public void ProcessTestNegativeNumber()
        {
            try
            {
                _registerEvent.Process(-1);
                Assert.Fail();
            }
            catch(Exception ex)
            {
                if (ex.GetType() != typeof(ArgumentException))
                    Assert.Fail();
            }
        }
        [TestMethod()]
        public void ProcessTestInvalidPositiveNumber()
        {
            try
            {
                _registerEvent?.Process(101);
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