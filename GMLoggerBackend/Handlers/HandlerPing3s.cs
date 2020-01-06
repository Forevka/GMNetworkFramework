using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;

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
