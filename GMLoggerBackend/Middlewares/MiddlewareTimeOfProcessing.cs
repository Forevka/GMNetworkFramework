using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Middlewares
{
    class MiddlewareTimeOfProcessing : IMiddleware
    {
        DateTime now;
        public void PreProcess(BaseRequestModel model, UserModel user, SocketHelper mySocket)
        {
            DateTime now = DateTime.Now;
        }
        public void PostProcess(BaseRequestModel model, UserModel user, SocketHelper mySocket)
        {
            
        }

        
    }
}
