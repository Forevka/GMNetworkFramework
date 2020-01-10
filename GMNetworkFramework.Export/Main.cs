using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;

namespace GMNetworkFramework.Export
{
    public class Main
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

        [DllImport("kernel32")]
        public static extern bool AllocConsole();

        [DllExport]
        public static bool OpenConsole()
        {
            return AllocConsole();
        }

        [DllExport]
        public static void LogLine(string a, int color)
        {
            Console.WriteLine(a, Color.FromKnownColor((KnownColor)color));
        }

        [DllExport]
        public static void Log(string a, int color)
        {
            Console.Write(a, Color.FromKnownColor((KnownColor)color));
        }
    }
}
