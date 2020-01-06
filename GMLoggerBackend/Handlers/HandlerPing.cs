using GMLoggerBackend.Enum;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using GMLoggerBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Handlers
{
    class HandlerPing : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, BufferStream buffer, SocketHelper mySocket, Dictionary<string, string> data)
        {
            //Send ping return to client.
            var responseModel = BaseResponseModel.Model<PingModelResponse>(ResponseFlag.Ping);
            responseModel.msg = "abrakadabra";

            Console.WriteLine($"Received ping from {mySocket.ClientIPAddress}");
            mySocket.SendMessage(responseModel);

            return data;
        }
    }
}
