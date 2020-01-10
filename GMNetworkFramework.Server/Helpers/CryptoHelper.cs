using GMNetworkFramework.Server.Security;

namespace GMNetworkFramework.Server.Helpers
{
    class CryptoHelper : ICrypto
    {
        public bool Initialized { get; set; } = false;

        private MyAesCrypto crypto;

        public CryptoHelper(string password, string salt, string vector)
            //: base(password, salt, vector)
        {
            crypto = new MyAesCrypto(password, salt, vector);
        }

        public byte[] DecryptBuffer(byte[] value)
        {
            return crypto.Decrypt(value);
        }

        public byte[] EncryptBuffer(byte[] value)
        {
            return crypto.Encrypt(value);
        }

        public void Initialize(string password)
        {
            Initialized = true;
        }
    }
}
