using GMLoggerBackend.Enums;
using GMLoggerBackend.Utils;
using GMLoggerBackend.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GMLoggerBackend.Models
{
    public class BaseRequestModel : PropertyFinderMixine
    {
        private BufferStream Buffer { get; set; }

        public ushort Flag { get; set; } = 0;

        public RequestFlag requestFlag { get { return (RequestFlag)Flag; }  }
        
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
            ushort _flag;
            Buffer.Read(out _flag);
            Flag = _flag;
        }

        public void ParseBuffer()
        {
            var thisProps = FindPropertyPosition();

            foreach (var prop in thisProps.OrderBy(x => x.Key))
            {
                Type prop_type = prop.Value.PropertyType;
                Console.WriteLine(prop_type.Name);
                if (prop_type == typeof(string))
                {
                    string val;
                    Buffer.Read(out val);
                    prop.Value.SetValue(this, val);
                }
                else if (prop_type == typeof(int))
                {
                    int val;
                    Buffer.Read(out val);
                    prop.Value.SetValue(this, val);
                }
                else if (prop_type == typeof(bool))
                {
                    bool val;
                    Buffer.Read(out val);
                    prop.Value.SetValue(this, val);
                }
                else if (prop_type == typeof(double))
                {
                    double val;
                    Buffer.Read(out val);
                    prop.Value.SetValue(this, val);
                }
                else if (prop_type == typeof(float))
                {
                    float val;
                    Buffer.Read(out val);
                    prop.Value.SetValue(this, val);
                }
            }      
        }
    }
}
