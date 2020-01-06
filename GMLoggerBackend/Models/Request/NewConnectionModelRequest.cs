using GMLoggerBackend.Utils.Attributes;

namespace GMLoggerBackend.Models.Request
{
    public class NewConnectionModelRequest : BaseRequestModel
    {
        [Position(0)]
        public string Ip { get; set; }

        [Position(1)]
        public string Name { get; set; }
    }
}
