using GMNetworkFramework.Server.Handlers;
using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Models;
using System;
using System.Collections.Generic;

namespace GMNetworkFramework.Example.Handlers
{
    class HandlerPing3s : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserBaseModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            Console.WriteLine($"user {user.Name} respond to every 3s ping from server");

            return data;
        }
    }
}
