using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Exceptions
{
    class CancelHandlerException : Exception
    {
        public CancelHandlerException(string message) : base(message)
        {
        }
    }
}
