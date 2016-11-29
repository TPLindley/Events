using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiagnoseEvent.Tests
{
    [TestClass()]
    public class DiagnoseTests
    {
        private Diagnose diagnoseEvent;
        [TestInitialize]
        public void Initialize()
        {
            diagnoseEvent = new Diagnose();
        }
        [TestMethod()]
        public void NameTest()
        {
            if (diagnoseEvent.Name().CompareTo(DiagnoseLibConstants.Name) != 0)
                Assert.Fail();
        }
        [TestMethod()]
        public void ProcessTest()
        {
            for (var index = 1; index < 101; index++)
            {
                var output = diagnoseEvent.Process(index);
                if (index % 2 == 0 && index % 7 == 0)
                {
                    if (output.CompareTo($"{DiagnoseLibConstants.Diagnose}{DiagnoseLibConstants.Patient}") != 0)
                        Assert.Fail();
                }
                else
                {
                    if (index % 2 == 0)
                    {
                        if (output.CompareTo(DiagnoseLibConstants.Diagnose) != 0)
                            Assert.Fail();
                    }
                    else if (index % 7 == 0)
                    {
                        if (output.CompareTo(DiagnoseLibConstants.Patient) != 0)
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
                var output = diagnoseEvent.Process(-1);
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
                var output = diagnoseEvent.Process(101);
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