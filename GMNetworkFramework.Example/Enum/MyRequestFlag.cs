using GMNetworkFramework.Server.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMNetworkFramework.Example.Enum
{
    enum MyRequestFlag : ushort
    {
        Disconnect = 2,
        NewConnection = 2000,
        Ping = 2004,
        PingResponse = 2005,
        Log = 2006,
    }
}
