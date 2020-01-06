using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;

namespace GMLoggerBackend.Handlers
{
    class HandlerUndefine : IHandler
    {
        public Dictionary<string, string> Process(BaseModel model, BufferStream buffer, SocketHelper mySocket, Dictionary<string, string> data)
        {

            return data;
        }
    }
}
