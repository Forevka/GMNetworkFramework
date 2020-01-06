using System;

namespace GMLoggerBackend.Exceptions
{
    class CancelHandlerException : Exception
    {
        public CancelHandlerException(string message) : base(message)
        {
        }
    }
}
