using GMLoggerBackend.Helpers;
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
        public Dictionary<string, string> Process(BufferStream buffer, SocketHelper mySocket, Dictionary<string, string> data)
        {
<<<<<<< Updated upstream
            var model = new LogModelRequest();
            model.FromBuffer(buffer);
            Console.WriteLine(model.msg);
=======
            //var model = new LogModelRequest();
            //model.FromBuffer(buffer);
            
            var this_model = model.ToModel<LogModelRequest>();
            Console.WriteLine(this_model.msg);
>>>>>>> Stashed changes
            return data;

        }
    }
}
