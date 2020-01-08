using GMLoggerBackend.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Models.Response
{
    class LogModelResponse : BaseResponseModel
    {
        [Position(0)]
        public List<string> msg { get; set; }
    }
}
