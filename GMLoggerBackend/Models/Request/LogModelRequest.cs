using GMLoggerBackend.Utils.Attributes;
using System.Collections.Generic;

namespace GMLoggerBackend.Models.Request
{
    public class LogModelRequest : BaseRequestModel
    {
        [Position(0)]
        public List<int> msg { get; set; }
    }
}
