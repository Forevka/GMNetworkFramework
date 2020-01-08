using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using GMLoggerBackend.Models.Request;
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
            return data;
        }
    }
}
