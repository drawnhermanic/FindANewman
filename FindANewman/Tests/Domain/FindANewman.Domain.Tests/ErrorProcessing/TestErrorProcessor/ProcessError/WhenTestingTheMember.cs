using NUnit.Framework;
using Rhino.Mocks;

namespace FindANewman.Domain.Tests.ErrorProcessing.TestErrorProcessor.ProcessError
{
    public class WhenTestingTheMember : WhenTestingTheClass
    {
        [TestFixtureSetUp]
        public void When()
        {
            Setup();
            ClassToTest.ProcessError(Exception);
        }

        [Test]
        public void ItShouldCallErrorLogger()
        {
            ExceptionLogger.AssertWasCalled(l => l.LogException(Exception));
        }
    }
}
