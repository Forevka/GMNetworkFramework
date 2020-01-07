using GMLoggerBackend.Enums;
using GMLoggerBackend.Handlers;
using GMLoggerBackend.Utils;
using NLog.Fluent;
using System;
using System.Globalization;

namespace GMLoggerBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            Console.WriteLine("Configuring Logger");
            Logger.Config();
            Console.WriteLine("Starting Server...");
            Server server = new Server();
            Logger.Debug($"Started at {DateTime.UtcNow}");
            Logger.Error(new Exception(), "test");

            server.RegisterHandler(RequestFlag.Undefined, new HandlerUndefine());
            server.RegisterHandler(RequestFlag.Disconnect, new HandlerDisconnect());

            server.RegisterHandler(RequestFlag.NewConnection, new HandlerNewConnection());

            server.RegisterHandler(RequestFlag.Ping, new HandlerPing());
            server.RegisterHandler(RequestFlag.PingResponse, new HandlerPing3s());

            server.RegisterHandler(RequestFlag.Log, new HandlerLog());

            server.StartServer(10103);
            Console.WriteLine("Server Started!");
        }
    }
}
