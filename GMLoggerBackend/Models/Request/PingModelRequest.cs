using GMLoggerBackend.Utils.Attributes;

namespace GMLoggerBackend.Models.Request
{
    class PingModelRequest : BaseRequestModel
    {
        [Position(0)]
        public float _float { get; set; }
    }
}
