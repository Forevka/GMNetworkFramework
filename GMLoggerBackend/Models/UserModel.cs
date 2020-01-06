using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Models
{
    public class UserModel
    {
        public string IpAddress { get; set; }

        public string Name { get; set; }

        public Guid Guid { get; set; }
    }
}
