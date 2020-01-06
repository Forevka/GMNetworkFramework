using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Models.Request
{
    public class LogModelRequest : BaseRequestModel
    {
        [Position(0)]
        public string msg { get; set; }
    }
}
