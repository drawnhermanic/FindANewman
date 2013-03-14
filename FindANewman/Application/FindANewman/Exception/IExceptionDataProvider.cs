using System.Web.Mvc;

namespace FindANewman.Exception
{
    public interface IExceptionDataProvider
    {
        ViewDataDictionary CreateErrorViewData(HandleErrorInfo modelException);
    }
}