using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using FindANewman.Controllers;
using Rhino.Mocks;
using Testing.Common;

namespace FindANewman.Tests.Controllers.TestAccountController
{
    public class WhenTestingTheController
    {
        protected AccountController Controller { get; set; }

        public void Setup()
        {
            Controller = new AccountController();
            Controller.ControllerContext = new ControllerContext(MvcAssert.BuildHttpContextStub(false), new RouteData(), Controller);
            
        }
    }
}
