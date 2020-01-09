using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Example.Models.Request;
using GMNetworkFramework.Example.Models.Response;
using System;
using System.Collections.Generic;
using GMNetworkFramework.Server.Handlers;
using GMNetworkFramework.Example.Enum;

namespace GMNetworkFramework.Example.Handlers
{
    class HandlerLog : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            var this_model = model.ToModel<LogModelRequest>();
            foreach(var i in this_model.msg)
                Console.WriteLine("msg is:" + i);

            var resp = BaseResponseModel.Model<LogModelResponse>((ushort)MyResponseFlag.LogResponse);
            resp.msg = new List<string>() { "qwe", "asd" };//mySocket.ParentServer.Clients.Count;

            mySocket.SendMessage(resp);

            return data;
        }
    }
}
