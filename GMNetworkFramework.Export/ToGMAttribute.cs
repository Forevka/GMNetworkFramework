using System;

namespace GMNetworkFramework.Export
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ToGMAttribute : Attribute
    {
        public ToGMAttribute(string externalName, string help)
        {
            ExternalName = externalName;
            Help = help;
        }

        public ToGMAttribute(string help)
        {
            this.Help = help;
        }

        public ToGMAttribute()
        {
        }

        public static GMType TypeFromString(string typeName)
        {
            if (typeName == "Double")
                return GMType.Double;
            return GMType.String;
        }

        public string Name { get; set; }
        public string ExternalName { get; set; }

        public string Help { get; set; } = "";

        public GMType ReturnType { get; set; }

        public int ArgCount { get; set; }

        public GMType[] Args { get; set; }
    }

    public enum GMType
    {
        String = 1,
        Double = 2,
    }
}
