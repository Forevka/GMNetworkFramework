using GMLoggerBackend.Enums;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using GMLoggerBackend.Models.Request;
using GMLoggerBackend.Models.Response;
using System;
using System.Collections.Generic;

namespace GMLoggerBackend.Handlers
{
    class HandlerLog : IHandler
    {
        public Dictionary<string, string> Process(BaseRequestModel model, UserModel user, SocketHelper mySocket, Dictionary<string, string> data)
        {
            var this_model = model.ToModel<LogModelRequest>();
            foreach(var i in this_model.msg)
                Console.WriteLine("msg is:" + i);

            var resp = BaseResponseModel.Model<LogModelResponse>(ResponseFlag.LogResponse);
            resp.msg = new List<string>() { "qwe", "asd" };//mySocket.ParentServer.Clients.Count;

            mySocket.SendMessage(resp);

            return data;
        }
    }
}
