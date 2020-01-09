using System;

namespace GMNetworkFramework.Server.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Position : Attribute
    {
        public int pos { get; set; }

        public Position(int _pos)
        {
            this.pos = _pos;
        }
    }
}
