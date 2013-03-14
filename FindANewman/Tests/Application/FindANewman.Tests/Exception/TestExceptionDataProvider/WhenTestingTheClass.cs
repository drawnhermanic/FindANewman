using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FindANewman.Exception;
using NUnit.Framework;

namespace FindANewman.Tests.Exception.TestExceptionDataProvider
{
    public abstract class WhenTestingTheClass
    {
        protected IExceptionDataProvider ClassToTest { get; set; }
        protected HandleErrorInfo ErrorInfo { get; set; }
        protected System.Exception Exception { get; set; }

        public void Setup()
        {
            ClassToTest = new ExceptionDataProvider();

            Exception = new System.Exception();
            ErrorInfo = new HandleErrorInfo(Exception, "controller", "action");
        }


    }
}
