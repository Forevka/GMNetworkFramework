using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Utils.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class Position : System.Attribute
    {
        public int pos { get; set; }

        public Position(int _pos)
        {
            this.pos = _pos;
        }
    }
}
