namespace GMNetworkFramework.Server.Security
{
    public interface ICrypto
    {
        bool Initialized { get; set; }

        void Initialize(string password);

        byte[] EncryptBuffer(byte[] value);

        byte[] DecryptBuffer(byte[] value);
    }
}
