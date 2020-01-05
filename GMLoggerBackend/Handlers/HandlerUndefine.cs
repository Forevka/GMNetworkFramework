using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMLoggerBackend.Helpers;

namespace GMLoggerBackend.Handlers
{
    class HandlerUndefine : IHandler
    {
        public Dictionary<string, string> Process(BufferStream buffer, SocketHelper mySocket, Dictionary<string, string> data)
        {

            return data;
        }
    }
}
