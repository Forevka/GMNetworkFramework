using GMLoggerBackend.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Models.Response
{
    public class PlayersCountModelResponse : BaseResponseModel
    {
        [Position(0)]
        public int Count { get; set; }
    }
}
