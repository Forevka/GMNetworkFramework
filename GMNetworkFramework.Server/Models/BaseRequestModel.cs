using GMNetworkFramework.Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GMNetworkFramework.Server.Models
{
    public class BaseRequestModel : PropertyFinderMixine
    {
        private BufferStream Buffer { get; set; }

        public ushort Flag { get; set; } = 0;
        
        public static BaseRequestModel FromStream(BufferStream buffer)
        {
            return new BaseRequestModel
            {
                Buffer = buffer.CloneBufferStream()
            };
        }

        public T ToModel<T>() where T: BaseRequestModel, new()
        {
            var newModel = new T
            {
                Buffer = Buffer.CloneBufferStream()
            };

            newModel.ParseBuffer();

            return newModel;
        }
        
        public void ParseFlag()
        {
            Buffer.Read(out ushort flag);
            Flag = flag;
        }

        public void ParseBuffer()
        {
            var thisProps = FindPropertyPosition();

            foreach (var prop in thisProps.OrderBy(x => x.Key))
            {
                Type propType = prop.Value.PropertyType;
                Console.WriteLine(propType.Name);

                if (IsEnumerable(prop.Value))
                {
                    Logger.Warn($"Found list of {propType.Name} in {prop.Value.Name}");

                    ParseListProperty(prop.Value);
                }
                else
                {
                    ParseOneProperty(prop.Value);
                }
            }      
        }

        private void ParseListProperty(PropertyInfo prop)
        {
            Buffer.Read(out ushort count);

            var elementType = prop.PropertyType.GetGenericArguments()[0];

            Logger.Warn($"Generic type {elementType.Name} count {count}");

            if (elementType == typeof(string))
            {
                var list = new List<string>();
                for (var i = 0; i < count; i++)
                {
                    Buffer.Read(out string val);

                    list.Add(val);
                }
                prop.SetValue(this, list);
            }
            else if (elementType == typeof(int))
            {
                var list = new List<int>();
                for (var i = 0; i < count; i++)
                {
                    Buffer.Read(out int val);

                    list.Add(val);
                }
                prop.SetValue(this, list);
            }
            else if (elementType == typeof(bool))
            {
                var list = new List<bool>();
                for (var i = 0; i < count; i++)
                {
                    Buffer.Read(out bool val);

                    list.Add(val);
                }
                prop.SetValue(this, list);
            }
            else if (elementType == typeof(double))
            {
                var list = new List<double>();
                for (var i = 0; i < count; i++)
                {
                    Buffer.Read(out double val);

                    list.Add(val);
                }
                prop.SetValue(this, list);
            }
            else if (elementType == typeof(float))
            {
                var list = new List<float>();
                for (var i = 0; i < count; i++)
                {
                    Buffer.Read(out float val);

                    list.Add(val);
                }
                prop.SetValue(this, list);
            }
            else
            {
                Logger.Warn($"Doesn't exist case for {prop.Name} on Request");
            }
        }

        private void ParseOneProperty(PropertyInfo prop)
        {
            var propType = prop.PropertyType;

            if (propType == typeof(string))
            {
                Buffer.Read(out string val);
                prop.SetValue(this, val);
            }
            else if (propType == typeof(int))
            {
                Buffer.Read(out int val);
                prop.SetValue(this, val);
            }
            else if (propType == typeof(bool))
            {
                Buffer.Read(out bool val);
                prop.SetValue(this, val);
            }
            else if (propType == typeof(double))
            {
                Buffer.Read(out double val);
                prop.SetValue(this, val);
            }
            else if (propType == typeof(float))
            {
                Buffer.Read(out float val);
                prop.SetValue(this, val);
            }
            else
            {
                Logger.Warn($"Doesn't exist case for {prop.Name} on Request");
            }
        }
    }
}
