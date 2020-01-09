using GMLoggerBackend.Enums;
using GMLoggerBackend.Handlers;
using GMLoggerBackend.Utils;
using GMLoggerBackend.Middlewares;
using NLog.Fluent;
using System;
using System.Globalization;
using GMLoggerBackend.Logic;
using GMLoggerBackend.Helpers;

namespace GMLoggerBackend
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

            Server server = new Server();

            //server.SetCryptoPolicy(new CryptoHelper(pass, salt, vector));
            //server.InitializeCrypto(pass, true);

            Logger.Debug($"Started at {DateTime.UtcNow}");

            var disp = server.GetMainDispatcher();

            disp.RegisterMiddleware(typeof(MiddlewareLogTime));

            disp.RegisterHandler(RequestFlag.Undefined, new HandlerUndefine());
            disp.RegisterHandler(RequestFlag.Disconnect, new HandlerDisconnect());

            var logicDisp = new Dispatcher("Logic");

            //logicDisp.RegisterMiddleware(typeof(MiddlewareLogTime));

            logicDisp.RegisterHandler(RequestFlag.NewConnection, new HandlerNewConnection());

            logicDisp.RegisterHandler(RequestFlag.Ping, new HandlerPing());
            logicDisp.RegisterHandler(RequestFlag.PingResponse, new HandlerPing3s());

            logicDisp.RegisterHandler(RequestFlag.Log, new HandlerLog());

            disp.AttachDispatcher(logicDisp);

            server.StartServer(10103);

            Console.WriteLine("Server Started!");
        }
    }
}
