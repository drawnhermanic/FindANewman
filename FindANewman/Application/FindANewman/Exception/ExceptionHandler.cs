
using System.Web.Mvc;
using FindANewman.Domain.ErrorProcessing;

namespace FindANewman.Exception
{
    public class ExceptionHandler : IExceptionHandler
    {
        public IErrorProcessor ErrorProcessor { get; set; }
        public IExceptionDataProvider ExceptionDataProvider { get; set; }

        public ExceptionHandler(IErrorProcessor errorProcessor, IExceptionDataProvider exceptionDataProvider)
        {
            ErrorProcessor = errorProcessor;
            ExceptionDataProvider = exceptionDataProvider;
        }

        #region Implementation of IExceptionHandler

        public void OnException(ExceptionContext exceptionContext)
        {
            ErrorProcessor.ProcessError(exceptionContext.Exception);
            RedirectToErrorViewWithCustomViewData(exceptionContext);
        }

        #endregion

        private void RedirectToErrorViewWithCustomViewData(ExceptionContext exceptionContext)
        {
            var controllerName = (string)exceptionContext.RouteData.Values["controller"];
            var actionName = (string)exceptionContext.RouteData.Values["action"];
            var modelException = new HandleErrorInfo(exceptionContext.Exception, controllerName, actionName);

            var result = new ViewResult
            {
                ViewName = "Error",
                ViewData = ExceptionDataProvider.CreateErrorViewData(modelException),
                TempData = exceptionContext.Controller.TempData
            };

            exceptionContext.Result = result;
            exceptionContext.ExceptionHandled = true;
            exceptionContext.HttpContext.Response.Clear();
            exceptionContext.HttpContext.Response.StatusCode = 500;
            exceptionContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

    }
}