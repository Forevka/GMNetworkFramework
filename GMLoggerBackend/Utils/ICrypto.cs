using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Utils
{
    public interface ICrypto
    {
        bool Initialized { get; set; }

        void Initialize(string password);

        byte[] EncryptBuffer(byte[] value);

        byte[] DecryptBuffer(byte[] value);
    }
}
