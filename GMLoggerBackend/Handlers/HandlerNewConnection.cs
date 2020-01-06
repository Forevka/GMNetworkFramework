using GMLoggerBackend.Enums;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using GMLoggerBackend.Models.Request;
using GMLoggerBackend.Models.Response;
using System;
using System.Collections.Generic;

namespace GMLoggerBackend.Handlers
{
    class HandlerNewConnection : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            var thisModel = model.ToModel<NewConnectionModelRequest>();

            //Update client information.
            user.IpAddress = thisModel.Ip;
            user.Name = thisModel.Name;
            user.Guid = new Guid();

            //Console Message.
            Console.WriteLine(thisModel.Ip + $" connected. Name: {thisModel.Name}");
            Console.WriteLine(Convert.ToString(mySocket.ParentServer.Clients.Count) + " clients online.");

            var resp = BaseResponseModel.Model<PlayersCountModelResponse>(ResponseFlag.PlayersCount);
            resp.Count = mySocket.ParentServer.Clients.Count;

            mySocket.SendMessageToAll(resp);

            return data;
        }
    }
}
