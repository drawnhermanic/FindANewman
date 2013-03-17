using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using FindANewman.Common.Security;
using FindANewman.Controllers;
using FindANewman.Data.Repositories;
using Rhino.Mocks;
using Testing.Common;

namespace FindANewman.Tests.Controllers.TestAccountController
{
    public class WhenTestingTheController
    {
        protected AccountController Controller { get; set; }

        protected IMembershipService MembershipService { get; set; }

        public void Setup()
        {
            MembershipService = MockRepository.GenerateMock<IMembershipService>();

            Controller = new AccountController(MembershipService);
            Controller.ControllerContext = new ControllerContext(MvcAssert.BuildHttpContextStub(false), new RouteData(), Controller);
            
        }
    }
}
