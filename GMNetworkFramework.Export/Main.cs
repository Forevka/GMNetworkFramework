﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace GMNetworkFramework.Export
{
    public class Main
    {
        [ToGM()]
        [DllExport]
        public static double Add(double a, double b)
        {
            return a + b;
        }

        [ToGM()]
        [DllExport]
        public static double Minus(double a, double b)
        {
            return a - b;
        }

        [ToGM]
        [DllExport]
        public static double Mult(double a, double b)
        {
            return a * b;
        }

        [ToGM]
        [DllExport]
        public static string StringTest(string a, string b)
        {
            Console.WriteLine(a);
            Console.WriteLine(b);

            return a + b;
        }

        [ToGM]
        [DllExport]
        public static string String()
        {
            return "qwe";
        }

        [DllImport("kernel32.dll")]
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
