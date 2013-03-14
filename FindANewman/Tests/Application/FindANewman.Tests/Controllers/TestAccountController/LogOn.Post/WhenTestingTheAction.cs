using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FindANewman.Models.Account;
using NUnit.Framework;
using Testing.Common;

namespace FindANewman.Tests.Controllers.TestAccountController.LogOn.Post
{
    public class WhenTestingTheAction : WhenTestingTheController
    {

        protected ActionResult Result { get; set; }
        
        public LogOnViewModel ViewModel { get; set; }
        public string ReturnUrl { get; set; }

        public void GivenThat()
        {
            Setup();
            ViewModel = new LogOnViewModel { EmailAddress = "TestEmailAddress", Password = "TestPassword" };
            ReturnUrl = "TestReturnUrl";
        }
    }
}
