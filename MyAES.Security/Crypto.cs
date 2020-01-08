using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyAES.Security
{
    public class MyAesCrypto
    {
        #region Settings

        private static int _iterations = 2;
        private static int _keySize = 256;

        private static string _hash = "SHA1";

        private Encoding myEncoding = Encoding.UTF8;

        private string _salt; // Random
        private string _vector; // Random

        private byte[] _saltBytes;
        private byte[] _vectorBytes;

        private string _password;

        #endregion

        public MyAesCrypto(string password, string salt, string vector, Encoding encoding)
        {
            myEncoding = encoding;

            _password = password;

            _salt = salt;
            _vector = vector;

            _saltBytes = myEncoding.GetBytes(salt.ToArray());
            _vectorBytes = myEncoding.GetBytes(vector.ToArray());
        }

        public MyAesCrypto(string password, string salt, string vector)
        {
            _password = password;

            _salt = salt;
            _vector = vector;

            _saltBytes = myEncoding.GetBytes(salt.ToArray());
            _vectorBytes = myEncoding.GetBytes(vector.ToArray());
        }

        public byte[] Encrypt(byte[] value)
        {
            return Encrypt<AesManaged>(value, _password);
        }

        public byte[] Encrypt<T>(byte[] valueBytes, string password) where T : SymmetricAlgorithm, new()
        {
            byte[] encrypted;
            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes =
                    new PasswordDeriveBytes(password, _saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, _vectorBytes))
                {
                    using (MemoryStream to = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }

            return encrypted;
        }

        public byte[] Decrypt(byte[] value)
        {
            return Decrypt<AesManaged>(value, _password);
        }

        public byte[] Decrypt<T>(byte[] valueBytes, string password) where T : SymmetricAlgorithm, new()
        {
            byte[] decrypted;
            int decryptedByteCount = 0;

            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, _saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                try
                {
                    using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, _vectorBytes))
                    {
                        using (MemoryStream from = new MemoryStream(valueBytes))
                        {
                            using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                            {
                                decrypted = new byte[valueBytes.Length];
                                decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Array.Empty<byte>();
                }

                cipher.Clear();
            }

            return decrypted;
        }

    }
}
