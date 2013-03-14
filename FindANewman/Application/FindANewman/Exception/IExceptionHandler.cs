using System.Web.Mvc;

namespace FindANewman.Exception
{
    public interface IExceptionHandler
    {
        void OnException(ExceptionContext exceptionContext);
    }
}