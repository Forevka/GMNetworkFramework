using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using GMLoggerBackend.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Handlers
{
    class HandlerLog : IHandler
    {
        public Dictionary<string, string> Process(BaseModel model, BufferStream buffer, SocketHelper mySocket, Dictionary<string, string> data)
        {
            //var model = new LogModelRequest();
            //model.FromBuffer(buffer);
            //Console.WriteLine(model.msg);
            return data;

        }
    }
}
