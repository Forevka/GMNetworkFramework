using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Server.Utils.Attributes;

namespace GMNetworkFramework.Example.Models.Response
{
    public class PlayersCountModelResponse : BaseResponseModel
    {
        [Position(0)]
        public int Count { get; set; }
    }
}
