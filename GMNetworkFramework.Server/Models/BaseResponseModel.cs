using GMNetworkFramework.Server.Enums;
using GMNetworkFramework.Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GMNetworkFramework.Server.Models
{
    public class BaseResponseModel : PropertyFinderMixine
    {
        public BufferStream Buffer { get; set; }

        public ushort Flag { get; set; } = 0;

        public static T Model<T>(ushort flag) where T : BaseResponseModel, new()
        {
            var newModel = new T
            {
                Flag = flag,

                Buffer = new BufferStream(BufferType.BufferSize, BufferType.BufferAlignment)
            };

            newModel.Buffer.Seek(0);

            newModel.Write(flag);

            return newModel;
        }

        public void ComposeBuffer()
        {
            var thisProps = FindPropertyPosition();

            foreach (var prop in thisProps.OrderBy(x => x.Key))
            {
                Type propType = prop.Value.PropertyType;
                Console.WriteLine(propType.Name);
                
                if (IsEnumerable(prop.Value))
                {
                    Logger.Warn($"Found list of {propType.Name} in {prop.Value.Name}");

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
                if (!(prop.GetValue(this) is List<string> collection)) return;

                var count = (ushort)collection.Count;

                Logger.Warn($"Count {count}");

                Buffer.Write(count);

                foreach(var val in collection)
                {
                    Buffer.Write(val);
                }
            }
            else if (elementType == typeof(double))
            {
                if (!(prop.GetValue(this) is List<double> collection)) return;

                var count = (ushort)collection.Count;

                Logger.Warn($"Count {count}");

                Buffer.Write(count);

                foreach (var val in collection)
                {
                    Buffer.Write(val);
                }
            }
            else if (elementType == typeof(int))
            {
                if (!(prop.GetValue(this) is List<int> collection)) return;

                var count = (ushort)collection.Count;

                Logger.Warn($"Count {count}");

                Buffer.Write(count);

                foreach (var val in collection)
                {
                    Buffer.Write(val);
                }
            }
            else if (elementType == typeof(bool))
            {
                if (!(prop.GetValue(this) is List<bool> collection)) return;

                var count = (ushort)collection.Count;

                Logger.Warn($"Count {count}");

                Buffer.Write(count);

                foreach (var val in collection)
                {
                    Buffer.Write(val);
                }
            }
            else if (elementType == typeof(float))
            {
                if (!(prop.GetValue(this) is List<float> collection)) return;

                var count = (ushort)collection.Count;

                Logger.Warn($"Count {count}");

                Buffer.Write(count);

                foreach (var val in collection)
                {
                    Buffer.Write(val);
                }
            }
        }

        private void ComposeOneProperty(PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(string))
            {
                Buffer.Write((string)prop.GetValue(this));
            }
            else if (prop.PropertyType == typeof(int))
            {
                Buffer.Write((int)prop.GetValue(this));
            }
            else if (prop.PropertyType == typeof(bool))
            {
                Buffer.Write((bool)prop.GetValue(this));
            }
            else if (prop.PropertyType == typeof(double))
            {
                Buffer.Write((double)prop.GetValue(this));
            }
            else if (prop.PropertyType == typeof(float))
            {
                Buffer.Write((float)prop.GetValue(this));
            }
            else
            {
                Logger.Warn($"Doesn't exist case for {prop.Name} on Response");
            }
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">BOOLEAN value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(bool value)
        {
            this.Buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">BYTE value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(byte value)
        {
            this.Buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">SBYTE value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(sbyte value)
        {
            this.Buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">USHORT value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(ushort value)
        {
            this.Buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">SHORT value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(short value)
        {
            this.Buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">UINT value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(uint value)
        {
            this.Buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">INT value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(int value)
        {
            this.Buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">FLOAT value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(float value)
        {
            this.Buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">DOUBLE value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(double value)
        {
            this.Buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">STRING value to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(string value)
        {
            this.Buffer.Write(value);
        }

        /// <summary>
        /// Writes a value of the specified type to this model.
        /// </summary>
        /// <param name="value">BYTE[] array to be written.</param>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void Write(byte[] value)
        {
            this.Buffer.Write(value);
        }
    }
}
