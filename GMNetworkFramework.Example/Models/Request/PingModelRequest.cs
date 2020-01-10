using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Server.Utils.Attributes;

namespace GMNetworkFramework.Example.Models.Request
{
    class PingModelRequest : BaseRequestModel
    {
        [Position(0)]
        public string Ping { get; set; }
    }
}
