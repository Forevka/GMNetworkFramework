using GMNetworkFramework.Server.Enums;
using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Logic;
using GMNetworkFramework.Server.Middlewares;
using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMNetworkFramework.Example.Middlewares
{
    class MiddlewareLogTime : IMiddleware
    {
        Dispatcher myDispatcher;
        DateTime start;

        List<ushort> IMiddleware.Flags => new List<ushort>() { (ushort)RequestFlag.ForAll };

        public bool ignoreUnhandled => false;

        public void PreProcess(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            start = DateTime.UtcNow;
        }

        public void PostProcess(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            Logger.Debug($"FROM: {myDispatcher.Name} Time for processing {DateTime.UtcNow - start}");
        }

        public void OnStart(Dispatcher masterDispatcher)
        {
            myDispatcher = masterDispatcher;
        }

        public void Dispose()
        {
            //disposing
        }
    }
}
