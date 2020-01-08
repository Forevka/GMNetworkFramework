using GMLoggerBackend.Enums;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Logic;
using GMLoggerBackend.Models;
using GMLoggerBackend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Middlewares
{
    class MiddlewareLogTime : IMiddleware
    {
        Dispatcher myDispatcher;
        DateTime start;

        List<RequestFlag> IMiddleware.Flags => new List<RequestFlag>() { RequestFlag.ForAll };

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
