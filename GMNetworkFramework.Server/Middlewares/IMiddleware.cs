using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Logic;
using GMNetworkFramework.Server.Models;
using System;
using System.Collections.Generic;



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
