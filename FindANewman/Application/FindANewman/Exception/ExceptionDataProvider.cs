using System.Web.Mvc;
using FindANewman.Models.Exception;

namespace FindANewman.Exception
{
    public class ExceptionDataProvider : IExceptionDataProvider
    {
        public ViewDataDictionary CreateErrorViewData(HandleErrorInfo modelException)
        {
            IExceptionViewModel model = new ExceptionViewModel
            {
                HandleErrorInfo = modelException
            };

            return new ViewDataDictionary<IExceptionViewModel>(model);
        }
    }
}