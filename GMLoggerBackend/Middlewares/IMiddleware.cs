using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;



namespace GMLoggerBackend.Middlewares
{
    public interface IMiddleware
    {
        void PreProcess(BaseRequestModel model, UserModel user, SocketHelper mySocket);

        void PostProcess(BaseRequestModel model, UserModel user, SocketHelper mySocket);
    }
}
