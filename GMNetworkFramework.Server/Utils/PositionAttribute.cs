using System;

namespace GMNetworkFramework.Server.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Position : Attribute
    {
        public int Pos { get; set; }

        public Position(int pos)
        {
            this.Pos = pos;
        }
    }
}
