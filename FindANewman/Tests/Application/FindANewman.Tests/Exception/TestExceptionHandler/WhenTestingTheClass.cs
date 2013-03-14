using FindANewman.Domain.ErrorProcessing;
using FindANewman.Exception;
using Rhino.Mocks;

namespace FindANewman.Tests.Exception.TestExceptionHandler
{
    public abstract class WhenTestingTheClass
    {
        protected IExceptionHandler ClassToTest { get; set; }

        protected IErrorProcessor ErrorProcessor { get; set; }
        
        protected IExceptionDataProvider ExceptionDataProvider { get; set; }

        public void Setup()
        {
            ErrorProcessor = MockRepository.GenerateMock<IErrorProcessor>();
            ExceptionDataProvider = MockRepository.GenerateMock<IExceptionDataProvider>();

            ClassToTest = new ExceptionHandler(ErrorProcessor, ExceptionDataProvider);
        }
    }
}
