using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using System;
using System.Collections.Generic;

namespace GMLoggerBackend.Handlers
{
    class HandlerPing3s : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            Console.WriteLine($"user {user.Name} respond to every 3s ping from server");

            return data;
        }
    }
}
