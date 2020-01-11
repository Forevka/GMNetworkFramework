using GMNetworkFramework.Export;
using GMNetworkFramework.ExportBuilder;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GMNetworkFramework.ExportBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var toGmExporter = new DllToGmExporter("GMNetworkFramework", "1.0.0", "GMNetworkFramework.Export.dll", "GMNetworkExtension.gmx", "GMLoggerClient");

            toGmExporter.ExportToExtension(Assembly.GetAssembly(typeof(Utils)));
            toGmExporter.MoveDllAndExt();

            Console.ReadKey();

        }
    }
}
