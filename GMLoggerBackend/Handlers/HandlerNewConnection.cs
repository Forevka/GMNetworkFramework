using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using GMLoggerBackend.Models.Request;

namespace GMLoggerBackend.Handlers
{
    class HandlerNewConnection : IHandler
    {
        public Dictionary<string, string> Process(BaseModel model, BufferStream buffer, SocketHelper mySocket, Dictionary<string, string> data)
        {
            var thisModel = model.ToModel<NewConnectionModelRequest>();

            //Update client information.
            mySocket.ClientIPAddress = thisModel.Ip;

            //Console Message.
            Console.WriteLine(thisModel.Ip + $" connected. Name: {thisModel.Name}");
            Console.WriteLine(Convert.ToString(mySocket.ParentServer.Clients.Count) + " clients online.");

            return data;
        }
    }
}
