using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindANewman.Domain.Entities
{
    public class ExceptionDetail : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual string Message { get; set; }
        public virtual string StackTrace { get; set; }
        public virtual DateTime CreateDateTime { get; set; }
    }
}
