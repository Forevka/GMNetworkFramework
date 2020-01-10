using GMNetworkFramework.Server.Utils.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GMNetworkFramework.Server.Utils
{
    public class PropertyFinderMixine
    {
        public Dictionary<int, PropertyInfo> FindPropertyPosition()
        {
            var props = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            var thisProps = new Dictionary<int, PropertyInfo>();

            foreach (var prop in props)
            {
                var attrs = System.Attribute.GetCustomAttributes(prop, typeof(Position));

                foreach (var attr in attrs)
                {
                    var p = (Position)attr;

                    thisProps.Add(p.Pos, prop);
                }
            }

            return thisProps;
        }

        public bool IsEnumerable(PropertyInfo prop)
        {
            return prop.PropertyType.GetInterfaces().Contains(typeof(IList));
        }
    }
}
