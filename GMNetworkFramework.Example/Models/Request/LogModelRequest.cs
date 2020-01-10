using System.Collections.Generic;
using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Server.Utils.Attributes;

namespace GMNetworkFramework.Example.Models.Request
{
    public class LogModelRequest : BaseRequestModel
    {
        [Position(0)]
        public List<int> msg { get; set; }
    }
}
