using GMLoggerBackend.Utils.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Utils
{
    public class PropertyFinderMixine
    {
        public Dictionary<int, PropertyInfo> FindPropertyPosition()
        {
            var props = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            var thisProps = new Dictionary<int, PropertyInfo>();

            foreach (var prop in props)
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(prop, typeof(Position));

                foreach (System.Attribute attr in attrs)
                {
                    Position p = (Position)attr;

                    thisProps.Add(p.pos, prop);
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
