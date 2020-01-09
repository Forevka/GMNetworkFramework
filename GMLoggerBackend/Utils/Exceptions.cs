using System;

namespace GMNetworkFramework.Server.Exceptions
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
