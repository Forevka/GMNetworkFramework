using GMLoggerBackend.Enums;
using GMLoggerBackend.Utils;
using GMLoggerBackend.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GMLoggerBackend.Models
{
    public class BaseResponseModel : PropertyFinderMixine
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
            var thisProps = FindPropertyPosition();

            foreach (var prop in thisProps.OrderBy(x => x.Key))
            {
                Type prop_type = prop.Value.PropertyType;
                Console.WriteLine(prop_type.Name);
                
                if (IsEnumerable(prop.Value))
                {
                    Logger.Warn($"Found list of {prop_type.Name} in {prop.Value.Name}");

                    ComposeListProperty(prop.Value);
                }
                else
                {
                    ComposeOneProperty(prop.Value);
                }
            }
        }

        private void ComposeListProperty(PropertyInfo prop)
        {
            var elementType = prop.PropertyType.GetGenericArguments()[0];

            Logger.Warn($"Generic type {elementType.Name}");

            if (elementType == typeof(string))
            {
                var collection = prop.GetValue(this) as List<string>;

                ushort count = (ushort)collection.Count;

                Logger.Warn($"Count {count}");

                _buffer.Write(count);

                foreach(var val in collection)
                {
                    _buffer.Write(val);
                }
            }
        }

        private void ComposeOneProperty(PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(string))
            {
                _buffer.Write((string)prop.GetValue(this));
            }
            else if (prop.PropertyType == typeof(int))
            {
                _buffer.Write((int)prop.GetValue(this));
            }
            else if (prop.PropertyType == typeof(bool))
            {
                _buffer.Write((bool)prop.GetValue(this));
            }
            else if (prop.PropertyType == typeof(double))
            {
                _buffer.Write((double)prop.GetValue(this));
            }
            else if (prop.PropertyType == typeof(float))
            {
                _buffer.Write((float)prop.GetValue(this));
            }
            else
            {
                Logger.Warn($"Doesnt exist case for {prop.Name} on Response");
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
