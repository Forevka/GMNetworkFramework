using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Server.Handlers;
using System;
using System.Collections.Generic;

namespace GMNetworkFramework.Example.Handlers
{
    class HandlerUndefine : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {

            return data;
        }
    }
}
