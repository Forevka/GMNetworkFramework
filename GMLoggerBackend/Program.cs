using GMLoggerBackend.Enums;
using GMLoggerBackend.Handlers;
using System;

namespace GMLoggerBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Server...");
            Server server = new Server();

            server.RegisterHandler(RequestFlag.Log, new HandlerLog());
            server.RegisterHandler(RequestFlag.Undefined, new HandlerUndefine());

            server.RegisterHandler(RequestFlag.NewConnection, new HandlerNewConnection());

            server.RegisterHandler(RequestFlag.Ping, new HandlerPing());
            server.RegisterHandler(RequestFlag.PingResponse, new HandlerPing3s());

            server.StartServer(10103);
            Console.WriteLine("Server Started!");
        }
    }
}
