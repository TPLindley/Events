using System;
using DiagnoseEvent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegisterEventTests
{
    [TestClass()]
    public class DiagnoseTests
    {
        private Diagnose _diagnoseEvent;
        [TestInitialize]
        public void Initialize()
        {
            _diagnoseEvent = new Diagnose();
        }
        [TestMethod()]
        public void NameTest()
        {
            if (String.Compare(_diagnoseEvent.Name(), DiagnoseLibConstants.Name, StringComparison.Ordinal) != 0)
                Assert.Fail();
        }
        [TestMethod()]
        public void ProcessTest()
        {
            for (var index = 1; index < 101; index++)
            {
                var output = _diagnoseEvent.Process(index);
                if (index % 2 == 0 && index % 7 == 0)
                {
                    if (String.Compare(output, $"{DiagnoseLibConstants.Diagnose}{DiagnoseLibConstants.Patient}", StringComparison.Ordinal) != 0)
                        Assert.Fail();
                }
                else
                {
                    if (index % 2 == 0)
                    {
                        if (String.Compare(output, DiagnoseLibConstants.Diagnose.Trim(), StringComparison.Ordinal) != 0)
                            Assert.Fail();
                    }
                    else if (index % 7 == 0)
                    {
                        if (String.Compare(output, DiagnoseLibConstants.Patient.Trim(), StringComparison.Ordinal) != 0)
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
        [TestMethod]
        public void ProcessTestNegativeNumber()
        {
            try
            {
                _diagnoseEvent.Process(-1);
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
                _diagnoseEvent.Process(101);
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