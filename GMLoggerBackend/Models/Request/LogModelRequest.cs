using GMLoggerBackend.Utils.Attributes;

namespace GMLoggerBackend.Models.Request
{
    public class LogModelRequest : BaseRequestModel
    {
        [Position(0)]
        public string msg { get; set; }
    }
}
