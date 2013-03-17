using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindANewman.Domain.Entities
{
    public class User : EntityBase
    {
        public virtual string EmailAddress { get; set; }
    }
}
