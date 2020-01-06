using GMLoggerBackend.Enums;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using GMLoggerBackend.Models.Request;
using GMLoggerBackend.Models.Response;
using System;
using System.Collections.Generic;

namespace GMLoggerBackend.Handlers
{
    class HandlerPing : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            var thisModel = model.ToModel<PingModelRequest>();
            Console.WriteLine($"user float from ping {thisModel._float}");
            //Send ping return to client.
            var responseModel = BaseResponseModel.Model<PingModelResponse>(ResponseFlag.Ping);
            responseModel.msg = "abrakadabra";
            responseModel.ping = thisModel._float + 1;

            Console.WriteLine($"Received ping from {user.Name}");
            mySocket.SendMessage(responseModel);

            return data;
        }
    }
}
