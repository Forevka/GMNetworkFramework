using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMNetworkFramework.Server.Enums;
using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Logic;
using GMNetworkFramework.Server.Models;



namespace GMNetworkFramework.Server.Middlewares
{
    public interface IMiddleware : IDisposable
    {
        List<ushort> Flags { get; }

        void OnStart(Dispatcher masterDispatcher);

        void PreProcess(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data);

        void PostProcess(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data);
    }
}
