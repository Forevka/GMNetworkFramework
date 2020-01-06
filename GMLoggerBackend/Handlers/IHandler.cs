using GMLoggerBackend.Helpers;
using GMLoggerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Handlers
{
    public interface IHandler
    {
        Dictionary<string, string> Process(BaseRequestModel model, BufferStream buffer, SocketHelper mySocket, Dictionary<string, string> data);
    }
}
