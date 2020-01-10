using System.Collections.Generic;
using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Server.Utils.Attributes;

namespace GMNetworkFramework.Example.Models.Response
{
    class LogModelResponse : BaseResponseModel
    {
        [Position(0)]
        public List<string> msg { get; set; }
    }
}
