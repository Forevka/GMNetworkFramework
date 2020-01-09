using GMNetworkFramework.Server.Utils;
using GMNetworkFramework.Server.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMNetworkFramework.Server.Helpers
{
    class CryptoHelper : MyAesCrypto, ICrypto
    {
        public bool Initialized { get; set; } = false;

        public CryptoHelper(string password, string salt, string vector)
            : base(password, salt, vector)
        {

        }

        public byte[] DecryptBuffer(byte[] value)
        {
            return base.Decrypt(value);
        }

        public byte[] EncryptBuffer(byte[] value)
        {
            return base.Encrypt(value);
        }

        public void Initialize(string password)
        {
            Initialized = true;
        }
    }
}
