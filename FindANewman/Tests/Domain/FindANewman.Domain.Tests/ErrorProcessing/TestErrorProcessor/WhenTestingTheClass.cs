using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindANewman.Domain.ErrorProcessing;
using FindANewman.Domain.Logging;
using NUnit.Framework;
using Rhino.Mocks;

namespace FindANewman.Domain.Tests.ErrorProcessing.TestErrorProcessor
{    
    public abstract class WhenTestingTheClass
    {
        protected IErrorProcessor ClassToTest { get; set; }
        protected IExceptionLogger ExceptionLogger { get; set; }

        protected Exception Exception { get; set; }

        public void Setup()
        {
            ExceptionLogger = MockRepository.GenerateMock<IExceptionLogger>();

            ClassToTest = new ErrorProcessor(ExceptionLogger);

            Exception = new Exception();
        }


    }

   
}
