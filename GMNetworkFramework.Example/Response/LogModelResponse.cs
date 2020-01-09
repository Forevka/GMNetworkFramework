using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Server.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMNetworkFramework.Example.Models.Response
{
    class LogModelResponse : BaseResponseModel
    {
        [Position(0)]
        public List<string> msg { get; set; }
    }
}
