using System;

namespace GMNetworkFramework.Server.Models
{
    public class UserBaseModel
    {
        public string IpAddress { get; set; }

        public string Name { get; set; }

        public Guid Guid { get; } = Guid.NewGuid();
    }
}
