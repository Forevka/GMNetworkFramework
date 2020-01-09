using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Server.Utils.Attributes;
using System.Collections.Generic;

namespace GMNetworkFramework.Example.Models.Request
{
    public class LogModelRequest : BaseRequestModel
    {
        [Position(0)]
        public List<int> msg { get; set; }
    }
}
