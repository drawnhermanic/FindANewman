using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FindANewman.Models.Exception;
using NUnit.Framework;

namespace FindANewman.Tests.Exception.TestExceptionDataProvider.CreateErrorViewData
{
    public class WhenTestingTheMember : WhenTestingTheClass
    {

        protected ViewDataDictionary Result { get; set; }

        [TestFixtureSetUp]
        public void When()
        {
            Setup();
            Result = ClassToTest.CreateErrorViewData(ErrorInfo);
        }

        [Test]
        public void ItShouldCallReturnExpectedResult()
        {
            Assert.IsInstanceOf<ViewDataDictionary<IExceptionViewModel>>(Result);
        }

        [Test]
        public void ItShouldReturnTheExpectedModelResult()
        {
            Assert.IsInstanceOf<IExceptionViewModel>(Result.Model);
        }

        [Test]
        public void ItShouldReturnTheExpecteErrorInfo()
        {
            var model = (IExceptionViewModel)Result.Model;
            Assert.AreEqual(ErrorInfo, model.HandleErrorInfo);
        }
    }
}
