using GMLoggerBackend.Utils.Attributes;

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
