using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMLoggerBackend.Helpers;

namespace GMLoggerBackend.Handlers
{
    class HandlerNewConnection : IHandler
    {
        public Dictionary<string, string> Process(BufferStream buffer, SocketHelper mySocket, Dictionary<string, string> data)
        {
            String ip;
            buffer.Read(out ip);

            //Update client information.
            mySocket.ClientIPAddress = ip;

            //Console Message.
            Console.WriteLine(ip + $" connected. IP: {ip}");
            Console.WriteLine(Convert.ToString(mySocket.ParentServer.Clients.Count) + " clients online.");

            return data;
        }
    }
}
