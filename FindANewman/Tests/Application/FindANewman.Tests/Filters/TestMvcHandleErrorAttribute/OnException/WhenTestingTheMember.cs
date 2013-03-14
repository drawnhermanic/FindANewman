using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;

namespace FindANewman.Tests.Filters.TestMvcHandleErrorAttribute.OnException
{
    public class WhenTestingTheMember : WhenTestingTheClass
    {
        public ExceptionContext Context { get; set; }

        [TestFixtureSetUp]
        protected void When()
        {
            Setup();
            ExceptionHandler.Stub(h => h.OnException(Context));

            ClassToTest.OnException(Context);
        }

        [Test]
        public void ItShouldCallErrorProcessor()
        {
            ExceptionHandler.AssertWasCalled(h => h.OnException(Context));
        }
    }
}
