using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FindANewman.Controllers;
using NUnit.Framework;
using Testing.Common;

namespace FindANewman.Tests.Controllers.TestAccountController.LogOn.Get
{
    public class WhenTestingTheAction : WhenTestingTheController
    {

        protected ActionResult Result { get; set; }

        [TestFixtureSetUp]
        public void When()
        {
            Setup();
            Result = Controller.LogOn();
        }

        [Test]
        public void ItShouldReturnTheExpectedResult()
        {
            Result.AssertIsDefaultView();
        }	
    }
}
