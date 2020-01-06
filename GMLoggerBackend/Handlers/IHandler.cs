using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using System.Collections.Generic;

namespace GMLoggerBackend.Handlers
{
    public interface IHandler
    {
        Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data);
    }
}
