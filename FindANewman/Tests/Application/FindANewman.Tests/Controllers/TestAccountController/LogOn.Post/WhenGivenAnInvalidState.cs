using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Testing.Common;

namespace FindANewman.Tests.Controllers.TestAccountController.LogOn.Post
{
    public class WhenGivenAnInvalidState : WhenTestingTheAction
    {

        [TestFixtureSetUp]
        public void When()
        {
            GivenThat();
            Controller.ModelState.AddModelError("", "");
            Result = Controller.LogOn(ViewModel, ReturnUrl);
        }

        [Test]
        public void ItShouldReturnTheExpectedResult()
        {
            Result.AssertIsDefaultView();
        }

        [Test]
        public void ItShouldHaveTheExpectedModelErrors()
        {
            Assert.AreEqual(1, Controller.ModelState.Count);
            Assert.AreEqual(1, Controller.ModelState[""].Errors.Count);
            Assert.AreEqual("", Controller.ModelState[""].Errors[0].ErrorMessage);
        }
    }
}
