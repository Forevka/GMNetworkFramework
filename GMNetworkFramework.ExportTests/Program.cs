using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMNetworkFramework.Export;
using System.Reflection;
using System.Xml;
using GMNetworkFramework.Tests;

namespace GMNetworkFramework.ExportTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var toGmExporter = new DllToGmExporter("GMNetworkFramework", "1.0.0", "GMNetworkFramework.Export.dll", "GMNetworkExtension.gmx");

            //toGmExporter.ExportToExtension(typeof(Utils));
            toGmExporter.MoveDllAndExt();

            Console.ReadKey();
        }
    }
}
