using GMNetworkFramework.Example.Enum;
using GMNetworkFramework.Example.Handlers;
using GMNetworkFramework.Example.Middlewares;
using GMNetworkFramework.Server.Enums;
using GMNetworkFramework.Server.Logic;
using GMNetworkFramework.Server.Utils;
//using NLog.Fluent;
using System;
using System.Globalization;

namespace GMNetworkFramework.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            string salt = "aselrias38490a32";
            string vector = "8947az34awl34kjq";

            string pass = "abcde";

            //Encoding utf8 = Encoding.UTF8;

            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            Console.WriteLine("Configuring Logger");
            Logger.Config();
            Console.WriteLine("Starting Server...");

            var server = new TcpServer();

            //server.SetCryptoPolicy(new CryptoHelper(pass, salt, vector));
            //server.InitializeCrypto(pass, true);

            Logger.Debug($"Started at {DateTime.UtcNow}");

            var disp = server.GetMainDispatcher();

            disp.RegisterMiddleware(typeof(MiddlewareLogTime));

            disp.RegisterHandler((ushort)RequestFlag.Undefined, new HandlerUndefine());
            disp.RegisterHandler((ushort)RequestFlag.Disconnect, new HandlerDisconnect());
            disp.RegisterHandler((ushort)MyRequestFlag.Ping, new HandlerPing());


            var logicDisp = new Dispatcher("Logic");

            //logicDisp.RegisterMiddleware(typeof(MiddlewareLogTime));

            logicDisp.RegisterHandler((ushort)MyRequestFlag.NewConnection, new HandlerNewConnection());

            logicDisp.RegisterHandler((ushort)MyRequestFlag.Ping, new HandlerPing());
            logicDisp.RegisterHandler((ushort)MyRequestFlag.PingResponse, new HandlerPing3s());

            logicDisp.RegisterHandler((ushort)MyRequestFlag.Log, new HandlerLog());

            disp.AttachDispatcher(logicDisp);

            server.StartServer(10103);

            Console.WriteLine("Server Started!");
        }
    }
}
