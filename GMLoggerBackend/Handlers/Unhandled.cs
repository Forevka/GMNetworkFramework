using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;

namespace GMLoggerBackend.Handlers
{
    class Unhandled : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            Console.WriteLine($"Unhandled event {model.Flag} from user {user.Guid}");
            return data;
        }
    }
}
