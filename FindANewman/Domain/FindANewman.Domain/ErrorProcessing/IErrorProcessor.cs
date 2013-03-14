using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindANewman.Domain.ErrorProcessing
{
    public interface IErrorProcessor
    {
        void ProcessError(Exception errorToProcess);
    }
}
