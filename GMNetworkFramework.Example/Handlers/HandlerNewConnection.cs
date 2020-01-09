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
    class HandlerNewConnection : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            var thisModel = model.ToModel<NewConnectionModelRequest>();

            //Update client information.
            user.IpAddress = mySocket.MscClient.Client.RemoteEndPoint.ToString();
            user.Name = thisModel.Name;

            //Console Message.
            Console.WriteLine(user.IpAddress + $" connected. Name: {thisModel.Name}");
            Console.WriteLine(Convert.ToString(mySocket.ParentServer.Clients.Count) + " clients online.");

            var resp = BaseResponseModel.Model<PlayersCountModelResponse>((ushort)MyResponseFlag.PlayersCount);
            resp.Count = mySocket.ParentServer.Clients.Count;

            mySocket.SendMessageToAll(resp);

            return data;
        }
    }
}
