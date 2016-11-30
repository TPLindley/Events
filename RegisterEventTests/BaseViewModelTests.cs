using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using EventBase.Interfaces;
using System.Threading.Tasks;
using Events.ViewModel;

namespace RegisterEventTests
{
    public class BaseTestModel : BaseViewModel
    {
        public BaseTestModel( ILogger logger):base(logger)
        {

        }
    }
    [TestClass()]
    public class BaseViewModelTests
    {
        public Mock<ILogger> _loggerMock = new Mock<ILogger>();
        public BaseTestModel _baseTestModel;

        [TestInitialize()]
        public void Initialize()
        {
            Exception ex = new ArgumentOutOfRangeException();
            _loggerMock
                .Setup(lm => lm.LogException(It.IsAny<string>(),It.IsAny<Exception>(),It.IsAny<string>()))
                .Returns(Task.FromResult(0));
            _baseTestModel = new BaseTestModel(_loggerMock.Object) { ShowErrors = false };
        }
        [TestMethod()]
        public void LogExceptionTest()
        {
            Exception ex = new ArgumentOutOfRangeException();
            _baseTestModel.LogException(ex);
            _loggerMock.Verify(lm => lm.LogException(It.IsAny<string>(), It.Is<Exception>(e => e == ex), It.IsAny<string>()), Times.Once());
        }
    }
}