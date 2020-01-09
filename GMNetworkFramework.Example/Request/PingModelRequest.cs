using GMNetworkFramework.Server.Utils.Attributes;
using GMNetworkFramework.Server.Models;

namespace GMNetworkFramework.Example.Models.Request
{
    class PingModelRequest : BaseRequestModel
    {
        [Position(0)]
        public float _float { get; set; }
    }
}
