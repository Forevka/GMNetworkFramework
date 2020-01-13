using GMNetworkFramework.Server.Handlers;
using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Models;
using System;
using System.Collections.Generic;
using GMNetworkFramework.Example.Models;

namespace GMNetworkFramework.Example.Handlers
{
    class Unhandled : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserBaseModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            Console.WriteLine($"Unhandled event {model.Flag} from user {user.Guid}");
            return data;
        }
    }
}
