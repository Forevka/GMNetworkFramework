using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace GMLoggerBackend.Helpers
{
    /// <summary>
    /// Handles clients. Reads and writes data and stores client information.
    /// </summary>
    public class SocketHelper
    {
        Queue<BufferStream> WriteQueue = new Queue<BufferStream>();
        public Thread ReadThread;
        public Thread WriteThread;
        public Thread AbortThread;
        public TcpClient MscClient;
        public Server ParentServer;
        public string ClientIPAddress;
        public string ClientName;
        public string ClientRace;
        public int ClientNumber;
        public bool IsSearching;
        public bool IsIngame;

        /// <summary>
        /// Starts the given client in two threads for reading and writing.
        /// </summary>
        public void StartClient(TcpClient client, Server server)
        {
            //Sets client variable.
            MscClient = client;
            MscClient.SendBufferSize = Server.BufferSize;
            MscClient.ReceiveBufferSize = Server.BufferSize;
            ParentServer = server;

            //Starts a read thread.
            ReadThread = new Thread(new ThreadStart(delegate
            {
                Read(client);
            }));
            ReadThread.Start();
            Console.WriteLine("Client read thread started.");

            //Starts a write thread.
            WriteThread = new Thread(new ThreadStart(delegate
            {
                Write(client);
            }));
            WriteThread.Start();
            Console.WriteLine("Client write thread started.");
        }

        /// <summary>
        /// Sends a string message to the client. This message is added to the write queue and send
        /// once it is it's turn. This ensures all messages are send in order they are given.
        /// </summary>
        public void SendMessage(BufferStream buffer)
        {
            WriteQueue.Enqueue(buffer);
        }

        /// <summary>
        /// Disconnects the client from the server and stops all threads for client.
        /// </summary>
        public void DisconnectClient()
        {
            //Console Message.
            Console.WriteLine("Disconnecting: " + ClientIPAddress);

            //Removes client from server.
            ParentServer.Clients.Remove(this);

            //Closes Stream.
            MscClient.Close();

            //Starts an abort thread.
            AbortThread = new Thread(new ThreadStart(delegate
            {
                Abort();
            }));
            Console.WriteLine("Aborting threads on client.");
            AbortThread.Start();
        }

        /// <summary>
        /// Handles aborting of threads.
        /// </summary>
        public void Abort()
        {
            //Stops Threads
            ReadThread.Abort();
            Console.WriteLine("Read thread aborted on client.");
            WriteThread.Abort();
            Console.WriteLine("Write thread aborted on client.");
            Console.WriteLine(ClientIPAddress + " disconnected.");
            Console.WriteLine(Convert.ToString(ParentServer.Clients.Count) + " clients online.");
            AbortThread.Abort();
        }

        /// <summary>
        /// Writes data to the client in sequence on the server.
        /// </summary>
        public void Write(TcpClient client)
        {
            while (true)
            {
                Thread.Sleep(10);
                if (WriteQueue.Count != 0)
                {
                    try
                    {
                        BufferStream buffer = WriteQueue.Dequeue();
                        NetworkStream stream = client.GetStream();
                        stream.Write(buffer.Memory, 0, buffer.Iterator);
                        stream.Flush();
                    }
                    catch (System.IO.IOException)
                    {
                        DisconnectClient();
                        break;
                    }
                    catch (NullReferenceException)
                    {
                        DisconnectClient();
                        break;
                    }
                    catch (ObjectDisposedException)
                    {
                        break;
                    }
                    catch (System.InvalidOperationException)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Reads data from the client and sends back a response.
        /// DISPATCHER
        /// </summary>
        public void Read(TcpClient client)
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(10);
                    BufferStream readBuffer = new BufferStream(Server.BufferSize, 1);
                    NetworkStream stream = client.GetStream();
                    stream.Read(readBuffer.Memory, 0, Server.BufferSize);

                    //Read the header data.
                    ushort constant;
                    readBuffer.Read(out constant);

                    //Determine input commmand.
                    switch (constant)
                    {
                        //New Connection
                        case 2000:
                        {
                            //Read out client data.
                            String ip;
                            readBuffer.Read(out ip);

                            //Update client information.
                            ClientIPAddress = ip;

                            //Console Message.
                            Console.WriteLine(ip + $" connected. Hash: {client.GetHashCode()}");
                            Console.WriteLine(Convert.ToString(ParentServer.Clients.Count) + " clients online.");
                            break;
                        }

                        //Recive client ping.
                        case 2004:
                        {
                            //Send ping return to client.
                            BufferStream buffer = new BufferStream(Server.BufferSize, Server.BufferAlignment);
                            buffer.Seek(0);
                            UInt16 constant_out = 1050;
                            buffer.Write(constant_out);
                            Console.WriteLine($"Received ping from {client.GetHashCode()}");
                            SendMessage(buffer);
                            break;
                        }

                        //Recive server ping.
                        case 2005:
                        {
                            //Nothing - Ping handled in ping thread.
                            break;
                        }
                    }
                }
                catch (System.IO.IOException)
                {
                    DisconnectClient();
                    break;
                }
                catch (NullReferenceException)
                {
                    DisconnectClient();
                    break;
                }
                catch (ObjectDisposedException)
                {
                    //Do nothing.
                    break;
                }
            }
        }
    }
}
