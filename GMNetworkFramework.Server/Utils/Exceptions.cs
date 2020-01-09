using System;
using System.Runtime.Serialization;

namespace GMNetworkFramework.Server.Exceptions
{
    class CancelHandlerException : Exception, ISerializable
    {
        public CancelHandlerException(string message) : base(message)
        {
        }
    }

    class StopProcessingException : Exception, ISerializable
    {
        public StopProcessingException(string message) : base(message)
        {
        }
    }
}
