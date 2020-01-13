using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMNetworkFramework.Server.Enums;
using GMNetworkFramework.Server.Handlers;
using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Example.Models.Response;
using GMNetworkFramework.Example.Enum;

namespace GMNetworkFramework.Example.Handlers
{
    class HandlerDisconnect : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserBaseModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            var resp = BaseResponseModel.Model<PlayersCountModelResponse>((ushort)MyResponseFlag.PlayersCount);
            resp.Count = mySocket.ParentServer.Clients.Count;

            mySocket.SendMessageToAll(resp);

            return data;
        }
    }
}
