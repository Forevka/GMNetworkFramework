using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using System;
using System.Collections.Generic;

namespace GMLoggerBackend.Handlers
{
    class HandlerUndefine : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {

            return data;
        }
    }
}
