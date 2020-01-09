using GMNetworkFramework.Server.Enums;
using GMNetworkFramework.Server.Utils;
using GMNetworkFramework.Server.Utils.Attributes;
using System;
using System.Collections;
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
            var m = new BaseRequestModel();
            m.Buffer = buffer.CloneBufferStream();

            return m;
        }

        public T ToModel<T>() where T: BaseRequestModel, new()
        {
            T new_model = new T();

            new_model.Buffer = Buffer.CloneBufferStream();
            new_model.ParseBuffer();

            return new_model;
        }
        
        public void ParseFlag()
        {
            Buffer.Read(out ushort _flag);
            Flag = _flag;
        }

        public void ParseBuffer()
        {
            var thisProps = FindPropertyPosition();

            foreach (var prop in thisProps.OrderBy(x => x.Key))
            {
                Type prop_type = prop.Value.PropertyType;
                Console.WriteLine(prop_type.Name);

                if (IsEnumerable(prop.Value))
                {
                    Logger.Warn($"Found list of {prop_type.Name} in {prop.Value.Name}");

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
                    double val;
                    Buffer.Read(out val);

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
                Logger.Warn($"Doesnt exist case for {prop.Name} on Request");
            }
        }

        private void ParseOneProperty(PropertyInfo prop)
        {
            var prop_type = prop.PropertyType;

            if (prop_type == typeof(string))
            {
                Buffer.Read(out string val);
                prop.SetValue(this, val);
            }
            else if (prop_type == typeof(int))
            {
                Buffer.Read(out int val);
                prop.SetValue(this, val);
            }
            else if (prop_type == typeof(bool))
            {
                Buffer.Read(out bool val);
                prop.SetValue(this, val);
            }
            else if (prop_type == typeof(double))
            {
                Buffer.Read(out double val);
                prop.SetValue(this, val);
            }
            else if (prop_type == typeof(float))
            {
                Buffer.Read(out float val);
                prop.SetValue(this, val);
            }
            else
            {
                Logger.Warn($"Doesnt exist case for {prop.Name} on Request");
            }
        }
    }
}
