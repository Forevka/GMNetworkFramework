using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Models.Request
{
    public class NewConnectionModelRequest : BaseModel
    {
        [Position(0)]
        public string Ip { get; set; }

        [Position(1)]
        public string Name { get; set; }
    }
}
