using GMLoggerBackend.Enum;
using GMLoggerBackend.Enums;
using GMLoggerBackend.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GMLoggerBackend.Models
{
    public class BaseResponseModel
    {
        public BufferStream _buffer { get; set; }

        public ushort Flag { get; set; } = 0;

        public RequestFlag requestFlag { get { return (RequestFlag)Flag; } }

        public static T Model<T>(ushort _flag) where T : BaseResponseModel, new()
        {
            T new_model = new T();

            new_model.Flag = _flag;

            new_model._buffer = new BufferStream(BufferType.BufferSize, BufferType.BufferAlignment);
            new_model._buffer.Seek(0);

            new_model.Write(_flag);

            return new_model;
        }

        public static T Model<T>(ResponseFlag _flag) where T : BaseResponseModel, new()
        {
            return Model<T>((ushort)_flag);
        }

        public void ComposeBuffer()
        {
            List<Position> positions = new List<Position>();

            var props = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            var thisProps = new Dictionary<int, PropertyInfo>();

            foreach (var prop in props)
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(prop, typeof(Position));
                // Displaying output.  
                foreach (System.Attribute attr in attrs)
                {
                    Position p = (Position)attr;

                    thisProps.Add(p.pos, prop);
                }
            }

            foreach (var prop in thisProps.OrderBy(x => x.Key))
            {
                Type prop_type = prop.Value.PropertyType;
                Console.WriteLine(prop_type.Name);
                if (prop_type == typeof(string))
                {
                    _buffer.Write((string)prop.Value.GetValue(this));
                }
                else if (prop_type == typeof(int))
                {
                    _buffer.Write((int)prop.Value.GetValue(this));
                }
                else if (prop_type == typeof(bool))
                {
                    _buffer.Write((bool)prop.Value.GetValue(this));
                }
                else if (prop_type == typeof(double))
                {
                    _buffer.Write((double)prop.Value.GetValue(this));
                }
                else if (prop_type == typeof(float))
                {
                    _buffer.Write((float)prop.Value.GetValue(this));
                }
            }
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">BOOLEAN value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(bool value)
        {
            this._buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">BYTE value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(byte value)
        {
            this._buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">SBYTE value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(sbyte value)
        {
            this._buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">USHORT value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(ushort value)
        {
            this._buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">SHORT value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(short value)
        {
            this._buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">UINT value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(uint value)
        {
            this._buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">INT value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(int value)
        {
            this._buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">FLOAT value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(float value)
        {
            this._buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">DOUBLE value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(double value)
        {
            this._buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">STRING value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(string value)
        {
            this._buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">BYTE[] array to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(byte[] value)
        {
            this._buffer.Write(value);
        }
    }
}
