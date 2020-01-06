using System;

namespace GMLoggerBackend.Models
{
    public class UserModel
    {
        public string IpAddress { get; set; }

        public string Name { get; set; }

        public Guid Guid { get; set; }
    }
}
