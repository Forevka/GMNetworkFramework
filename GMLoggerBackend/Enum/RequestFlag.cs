using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Enums
{
    public enum RequestFlag : ushort
    {
        Undefined = 0,
        NewConnection = 2000,
        Ping = 2004,
        PingResponse = 2005,
    }
}
