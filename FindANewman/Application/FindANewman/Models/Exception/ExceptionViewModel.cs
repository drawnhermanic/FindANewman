using System.Web.Mvc;

namespace FindANewman.Models.Exception
{
    public class ExceptionViewModel : IExceptionViewModel
    {
        public HandleErrorInfo HandleErrorInfo { get; set; }
    }
}