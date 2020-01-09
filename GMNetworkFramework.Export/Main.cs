using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMNetworkFramework.Export
{
    class Main
    {
        [DllExport]
        public static double Add(double a, double b)
        {
            return a + b;
        }

        [DllExport]
        public static double Minus(double a, double b)
        {
            return a - b;
        }

        [DllExport]
        public static double Mult(double a, double b)
        {
            return a * b;
        }

        [DllExport]
        public static string StringTest(string a, string b)
        {
            Console.WriteLine(a);
            Console.WriteLine(b);

            return a + b;
        }
    }
}
