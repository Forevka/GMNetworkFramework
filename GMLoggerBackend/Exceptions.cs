using System;

namespace GMLoggerBackend.Exceptions
{
    class CancelHandlerException : Exception
    {
        public CancelHandlerException(string message) : base(message)
        {
        }
    }

    class StopProcessingException : Exception
    {
        public StopProcessingException(string message) : base(message)
        {
        }
    }
}
