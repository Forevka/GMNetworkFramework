using GMLoggerBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Models
{
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class Position : System.Attribute
    {
        public int pos { get; set; }

        public Position(int _pos)
        {
            this.pos = _pos;
        }
    }
    

    public class BaseModel
    {
        //[Position(0)]
        private BufferStream _buffer { get; set; }

        public ushort Flag { get; set; } = 0;

        public RequestFlag requestFlag { get { return (RequestFlag)Flag; }  }
        
        public static BaseModel FromStream(BufferStream buffer)
        {
            var m = new BaseModel();
            m._buffer = buffer.CloneBufferStream();

            return m;
        }

        public T ToModel<T>() where T: BaseModel, new()
        {
            T new_model = new T();

            new_model._buffer = _buffer.CloneBufferStream();
            new_model.ParseBuffer();

            return new_model;
        }
        

        public void ParseFlag()
        {
            ushort _flag;
            _buffer.Read(out _flag);
            Flag = _flag;
        }

        public void ParseBuffer()
        {
            List<Position> positions = new List<Position>();

            var props = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            var thisProps = new Dictionary<int, PropertyInfo>();

            foreach (var prop in props)
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(prop);
                // Displaying output.  
                foreach (System.Attribute attr in attrs)
                {
                    if (attr is Position)
                    {
                        Position p = (Position)attr;
                        //Console.WriteLine(prop.Name);
                        //Console.WriteLine(p.pos);
                        thisProps.Add(p.pos, prop);
                    }
                }
            }

            foreach(var prop in thisProps.OrderBy(x => x.Key))
            {
                Type prop_type = prop.Value.PropertyType;
                Console.WriteLine(prop_type.Name);
                if (prop_type.Name == "String")
                {
                    String val;
                    _buffer.Read(out val);
                    prop.Value.SetValue(this, val);
                }
            }
        }
    }
}
