using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMLoggerBackend.Enums;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Logic;
using GMLoggerBackend.Models;



namespace GMLoggerBackend.Middlewares
{
    public interface IMiddleware : IDisposable
    {
        List<RequestFlag> Flags { get; }

        void OnStart(Dispatcher masterDispatcher);

        void PreProcess(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data);

        void PostProcess(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data);
    }
}
