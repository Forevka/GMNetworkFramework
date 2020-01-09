using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Models;
using System.Collections.Generic;

namespace GMNetworkFramework.Server.Handlers
{
    public interface IHandler
    {
        Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data);
    }
}
