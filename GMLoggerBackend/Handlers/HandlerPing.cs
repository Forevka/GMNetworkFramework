using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Handlers
{
    class HandlerPing : IHandler
    {
        public Dictionary<string, string> Process(BaseModel model, BufferStream buffer, SocketHelper mySocket, Dictionary<string, string> data)
        {
            //Send ping return to client.
            BufferStream responseBuffer = new BufferStream(Server.BufferSize, Server.BufferAlignment);
            responseBuffer.Seek(0);
            UInt16 constant_out = 1050;
            responseBuffer.Write(constant_out);

            Console.WriteLine($"Received ping from {mySocket.ClientIPAddress}");
            mySocket.SendMessage(responseBuffer);

            return data;
        }
    }
}
