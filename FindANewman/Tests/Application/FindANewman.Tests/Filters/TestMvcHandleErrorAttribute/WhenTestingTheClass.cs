using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindANewman.Exception;
using FindANewman.Filters;
using Rhino.Mocks;

namespace FindANewman.Tests.Filters.TestMvcHandleErrorAttribute
{
    public abstract class WhenTestingTheClass
    {
        protected IExceptionHandler ExceptionHandler { get; set; }

        protected MvcHandleErrorAttribute ClassToTest { get; set; }

        public void Setup()
        {
            ExceptionHandler = MockRepository.GenerateMock<IExceptionHandler>();

            ClassToTest = new MvcHandleErrorAttribute(ExceptionHandler);
        }

        
    }
}
