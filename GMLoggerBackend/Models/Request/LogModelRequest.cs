using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Models.Request
{
    public class LogModelRequest
    {
        public String msg { get; set; } = "";
        public void FromBuffer(BufferStream buffer)
        {
            String _msg; 
            buffer.Read(out _msg);
            if (_msg != null) msg = _msg;

        }
    }
}
