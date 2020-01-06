using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMLoggerBackend.Enum;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using GMLoggerBackend.Models.Response;

namespace GMLoggerBackend.Handlers
{
    class HandlerDisconnect : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            var resp = BaseResponseModel.Model<PlayersCountModelResponse>(ResponseFlag.PlayersCount);
            resp.Count = mySocket.ParentServer.Clients.Count;

            mySocket.SendMessageToAll(resp);

            return data;
        }
    }
}
