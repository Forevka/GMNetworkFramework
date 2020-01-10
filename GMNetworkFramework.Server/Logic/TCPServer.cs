using GMNetworkFramework.Server.Enums;
using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Security;
using GMNetworkFramework.Server.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GMNetworkFramework.Server.Logic
{
    public class TCPServer
    {
        public static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);

        public List<SocketHelper> Clients;
        public List<SocketHelper> SearchingClients;
        public Thread TcpThread;
        public Thread PingThread;
        public TcpListener TcpListener = null;

        public bool isCryptEnabled = false;

        private Dispatcher mainDispatcher;
        private ICrypto crypto;

        public void SetMainDispatcher(Dispatcher dispatcher)
        {
            mainDispatcher = dispatcher;
        }

        public Dispatcher GetMainDispatcher()
        {
            if (mainDispatcher == null)
                mainDispatcher = new Dispatcher("MAIN");

            return mainDispatcher;
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
            TcpThread = new Thread(new ThreadStart(delegate
            {
                Listen(tcpPort);
            }));
            TcpThread.Start();
            Console.WriteLine("Listen thread started.");

            //Starts a ping thread to keep connection alive.
            PingThread = new Thread(new ThreadStart(delegate
            {
                Ping();
            }));
            PingThread.Start();
            Console.WriteLine("Ping thread started.");
        }

        /// <summary>
        /// Stops the server from running.
        /// </summary>
        public void StopServer()
        {
            TcpListener.Stop();

            TcpThread.Abort();
            PingThread.Abort();
            //MatchmakingThread.Abort();

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

        public void InitializeCrypto(string password, bool enable)
        {
            isCryptEnabled = enable;

            if (!crypto.Initialized)
                crypto.Initialize(password);
        }

        public byte[] EncryptBuffer(BufferStream buffer)
        {
            if (isCryptEnabled)
                return crypto.EncryptBuffer(buffer.Memory);

            return buffer.Memory;
        }

        public byte[] DecryptBuffer(BufferStream buffer)
        {
            if (isCryptEnabled)
                return crypto.DecryptBuffer(buffer.Memory);

            return buffer.Memory;
        }

        public void SetCryptoPolicy(ICrypto crypto)
        {
            this.crypto = crypto;
        }

        public void EnableCryptoPolicy()
        {
            isCryptEnabled = true;
        }

        public void DisableCryptoPolicy()
        {
            isCryptEnabled = false;
        }

        /// <summary>
        /// Listens for clients and starts threads to handle them.
        /// </summary>
        private async void Listen(int port)
        {
            TcpListener = new TcpListener(IPAddress.Any, port);
            TcpListener.Start();

            while (true)
            {
                try
                {
                    var tcpClient = await TcpListener.AcceptTcpClientAsync();
                    SocketHelper helper = new SocketHelper
                    {
                        Me = new Models.UserModel()
                    };
                    Clients.Add(helper);

                    helper.StartClient(tcpClient, this, mainDispatcher);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, ex.Message);
                }
            }
        }
    }
}
