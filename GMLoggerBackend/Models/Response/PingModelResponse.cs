using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Models.Response
{
    class PingModelResponse : BaseResponseModel
    {
        [Position(0)]
        public string msg { get; set; }

        [Position(1)]
        public float ping { get; set; }
    }
}
