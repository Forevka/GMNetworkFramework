using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMNetworkFramework.Export
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ToGMAttribute : Attribute
    {
        public ToGMAttribute(string ExternalName, string Help)
        {
            this.ExternalName = ExternalName;
            this.Help = Help;
        }

        public ToGMAttribute(string Help)
        {
            this.Help = Help;
        }

        public ToGMAttribute()
        {
        }

        public static GMType TypeFromString(string typeName)
        {
            if (typeName == "Double")
                return GMType.Double;
            else
            {
                return GMType.String;
            }
        }

        public string Name { get; set; }
        public string ExternalName { get; set; }

        public string Help { get; set; } = "";

        public GMType ReturnType { get; set; }

        public int ArgCount { get; set; }

        public GMType[] args { get; set; }
    }

    public enum GMType:int
    {
        String = 1,
        Double = 2,
    }
}
