using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Middlewares
{
    public interface IMiddlewares
    {
        void Process(BaseRequestModel model, UserModel user, SocketHelper mySocket);
    }
}
