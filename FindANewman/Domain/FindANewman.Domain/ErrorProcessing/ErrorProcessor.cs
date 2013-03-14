using System;
using FindANewman.Domain.Logging;

namespace FindANewman.Domain.ErrorProcessing
{
    public class ErrorProcessor : IErrorProcessor
    {
        private IExceptionLogger ErrorLogger { get; set; }

        public ErrorProcessor(IExceptionLogger exceptionLogger)
        {
            ErrorLogger = exceptionLogger;
        }

        public void ProcessError(Exception errorToProcess)
        {
            ErrorLogger.LogException(errorToProcess);
        }
    }
}