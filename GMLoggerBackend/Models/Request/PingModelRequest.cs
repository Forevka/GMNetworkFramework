using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Models.Request
{
    class PingModelRequest : BaseRequestModel
    {
        [Position(0)]
        public float _float { get; set; }
    }
}
