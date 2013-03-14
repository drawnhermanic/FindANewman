using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Rhino.Mocks;
using Testing.Common;

namespace FindANewman.Tests.Exception.TestExceptionHandler.OnException
{
    public class WhenTestingTheMember : WhenTestingTheClass
    {
        protected ExceptionContext Context { get; set; }
        protected System.Exception Exception { get; set; }

        protected RouteData RouteData { get; set; }
        protected ViewDataDictionary ViewDataDictionary { get; set; }
        protected HandleErrorInfo HandleErrorInfo { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        protected ControllerBase Controller { get; set; }
        protected HttpContextBase HttpContext;
        protected HttpResponseBase HttpResponseBase;

        [TestFixtureSetUp]
        public void When()
        {
            Setup();
            GivenThat();
            ClassToTest.OnException(Context);
        }

        private void GivenThat()
        {
            HttpContext = MockRepository.GenerateStub<HttpContextBase>();
            HttpResponseBase = MockRepository.GenerateStub<HttpResponseBase>();
            HttpContext.Stub(h => h.Response).Return(HttpResponseBase);

            HttpContext = MvcAssert.BuildHttpContextStub(false);
            var httpResponseBase = MockRepository.GenerateStub<HttpResponseBase>();
            HttpContext.Stub(c => c.Response).Return(httpResponseBase);

            Exception = new System.Exception("test message");
            ActionName = "TestActionName";
            ControllerName = "TestControllerName";

            RouteData = new RouteData();
            RouteData.Values.Add("controller", ControllerName);
            RouteData.Values.Add("action", ActionName);
            Controller = new MockController();

            Context = new ExceptionContext
            {
                Exception = Exception,
                RouteData = RouteData,
                Controller = Controller,
                HttpContext = HttpContext
            };
            HandleErrorInfo = new HandleErrorInfo(Exception, ControllerName, ActionName);

            ErrorProcessor.Stub(p => p.ProcessError(Context.Exception));
            ExceptionDataProvider.Stub(p => p.CreateErrorViewData(HandleErrorInfo)).Return(ViewDataDictionary);
        }

        public class MockController : Controller
        {
            public ActionResult Action()
            {
                return View(string.Empty);
            }
        }

        [Test]
        public void ItShouldCallErrorProcessor()
        {
            ErrorProcessor.AssertWasCalled(a => a.ProcessError(Context.Exception));
        }

        [Test]
        public void ItShouldCallExceptionDataProvider()
        {
            ExceptionDataProvider.AssertWasCalled(d => d.CreateErrorViewData(Arg<HandleErrorInfo>
                .Matches(h => h.Exception == Exception
                    && h.ControllerName == ControllerName
                    && h.ActionName == ActionName)
                ));
        }

        [Test]
        public void ItShouldReturnTheExpectedViewResult()
        {
            Assert.IsInstanceOf<ViewResult>(Context.Result);
        }

        [Test]
        public void ItShouldReturnTheExpectedViewDataResult()
        {
            var viewResult = (ViewResult)Context.Result;
            Assert.IsInstanceOf<ViewDataDictionary>(viewResult.ViewData);
            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [Test]
        public void ItShouldReturnTheExpectedResult()
        {
            Assert.IsTrue(Context.ExceptionHandled);
            Assert.IsTrue(Context.HttpContext.Response.TrySkipIisCustomErrors);
            Assert.AreEqual(500, Context.HttpContext.Response.StatusCode);
        }
    }
}
