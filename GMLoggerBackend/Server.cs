using GMLoggerBackend.Enums;
using GMLoggerBackend.Handlers;
using GMLoggerBackend.Helpers;
using GMLoggerBackend.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GMLoggerBackend
{
    public class Server
    {
        public static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);

        public List<SocketHelper> Clients;
        public List<SocketHelper> SearchingClients;
        public Thread TCPThread;
        public Thread PingThread;
        public Thread MatchmakingThread;
        public TcpListener TCPListener = null;
        public Dictionary<RequestFlag, List<IHandler>> Handlers = new Dictionary<RequestFlag, List<IHandler>>();

        public void RegisterHandler(RequestFlag flag, IHandler handler)
        {
            List<IHandler> hList = null;

            if (Handlers.TryGetValue(flag, out hList))
                hList.Add(handler);
            else
            {
                Handlers.Add(flag, new List<IHandler>() { handler });
            }
        }

        /// <summary>
        /// Starts the server.
        /// </summary>
        public void StartServer(int tcpPort)
        {
            //Creates a client list.
            Clients = new List<SocketHelper>();
            SearchingClients = new List<SocketHelper>();

            //Starts a listen thread to listen for connections.
            TCPThread = new Thread(new ThreadStart(delegate
            {
                Listen(tcpPort);
            }));
            TCPThread.Start();
            Console.WriteLine("Listen thread started.");

            //Starts a ping thread to keep connection alive.
            PingThread = new Thread(new ThreadStart(delegate
            {
                Ping();
            }));
            PingThread.Start();
            Console.WriteLine("Ping thread started.");
        }

        // <summary>
        /// Stops the server from running.
        /// </summary>
        public void StopServer()
        {
            TCPListener.Stop();

            TCPThread.Abort();
            PingThread.Abort();
            MatchmakingThread.Abort();

            foreach (SocketHelper client in Clients)
            {
                client.MscClient.GetStream().Close();
                client.MscClient.Close();
                client.ReadThread.Abort();
                client.WriteThread.Abort();
            }

            Clients.Clear();
        }

        /// <summary>
        /// Constantly pings clients with messages to see if they disconnect.
        /// </summary>
        private void Ping()
        {
            //Send ping to clients every 3 seconds.
            while (true)
            {
                Thread.Sleep(3000);
                BufferStream buffer = new BufferStream(BufferType.BufferSize, BufferType.BufferAlignment);
                buffer.Seek(0);
                ushort constant_out = 1007;
                buffer.Write(constant_out);
                SendToAllClients(buffer);
            }
        }

        /// <summary>
        /// Sends a message out to all connected clients.
        /// </summary>
        public void SendToAllClients(BufferStream buffer)
        {
            foreach (SocketHelper client in Clients)
            {
                client.SendMessage(buffer);
            }
        }


        /// <summary>
        /// Listens for clients and starts threads to handle them.
        /// </summary>
        async private void Listen(int port)
        {
            TCPListener = new TcpListener(IPAddress.Any, port);
            TCPListener.Start();

            while (true)
            {
                try
                {
                    var tcpClient = await TCPListener.AcceptTcpClientAsync();
                    SocketHelper helper = new SocketHelper
                    {
                        Me = new Models.UserModel()
                    };
                    helper.StartClient(tcpClient, this);
                    Clients.Add(helper);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, ex.Message.ToString());
                }
            }
        }
    }
}
