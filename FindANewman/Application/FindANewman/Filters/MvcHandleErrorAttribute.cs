using System;
using System.Web.Mvc;
using FindANewman.Exception;

namespace FindANewman.Filters
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class MvcHandleErrorAttribute : HandleErrorAttribute
    {
        public IExceptionHandler ExceptionHandler { get; private set; }

        public MvcHandleErrorAttribute(IExceptionHandler exceptionHandler)
        {
            ExceptionHandler = exceptionHandler;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            ExceptionHandler.OnException(filterContext);
        }

    }
}