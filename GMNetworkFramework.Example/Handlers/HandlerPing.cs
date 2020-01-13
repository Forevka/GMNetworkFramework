using GMNetworkFramework.Example.Enum;
using GMNetworkFramework.Example.Models.Request;
using GMNetworkFramework.Example.Models.Response;
using GMNetworkFramework.Server.Handlers;
using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Models;
using System;
using System.Collections.Generic;

namespace GMNetworkFramework.Example.Handlers
{
    class HandlerPing : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserBaseModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            var thisModel = model.ToModel<PingModelRequest>();
            Console.WriteLine($"user float from ping {thisModel.Ping}");
            //Send ping return to client.
            var responseModel = BaseResponseModel.Model<PingModelResponse>((ushort)MyResponseFlag.Ping);
            responseModel.msg = "abrakadabra";
            responseModel.Ping = thisModel.Ping;

            Console.WriteLine($"Received ping from {user.Name}");
            mySocket.SendMessage(responseModel);

            return data;
        }
    }
}
