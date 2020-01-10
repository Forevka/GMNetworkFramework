using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMNetworkFramework.Export
{
    public class Utils
    {
        private static readonly DateTime UnixEpoch =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        [ToGM("GetCurrentTimestamp", "GetCurrentTimestamp(a, b)")]
        [DllExport]
        public static double GetCurrentTimestamp()
        {
            return (DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }

        [ToGM()]
        [DllExport]
        public static double Test(double a)
        {
            return 2;
        }
    }
}
