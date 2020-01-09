using GMNetworkFramework.Server.Enums;
using GMNetworkFramework.Server.Logic;
using GMNetworkFramework.Server.Middlewares;
using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Server.Utils;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace GMNetworkFramework.Server.Helpers
{
    /// <summary>
    /// Handles clients. Reads and writes data and stores client information.
    /// </summary>
    public class SocketHelper
    {
        Queue<BufferStream> WriteQueue = new Queue<BufferStream>();
        public Thread ReadThread;
        public Thread WriteThread;
        private Thread AbortThread;
        public TcpClient MscClient;
        public TCPServer ParentServer;
        public UserModel Me;

        private Dispatcher _dispatcher;
        public Dictionary<Guid, List<IMiddleware>> myMiddlewares;

        /// <summary>
        /// Starts the given client in two threads for reading and writing.
        /// </summary>
        public void StartClient(TcpClient client, TCPServer server, Dispatcher dispatcher)
        {
            //Sets client variable.
            MscClient = client;
            MscClient.SendBufferSize = (int)BufferType.BufferSize;
            MscClient.ReceiveBufferSize = (int)BufferType.BufferSize;
            ParentServer = server;

            _dispatcher = dispatcher;
            myMiddlewares = dispatcher.InitializeMiddlewares();

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

        public void SendMessageToAll(BufferStream buffer)
        {
            ParentServer.SendToAllClients(buffer);
        }

        public void SendMessageToAll(BaseResponseModel model)
        {
            model.ComposeBuffer();
            ParentServer.SendToAllClients(model._buffer);
        }

        /// <summary>
        /// Sends a string message to the client. This message is added to the write queue and send
        /// once it is it's turn. This ensures all messages are send in order they are given.
        /// </summary>
        public void SendMessage(BufferStream buffer)
        {
            WriteQueue.Enqueue(buffer);
        }

        public void SendMessage(BaseResponseModel model)
        {
            model.ComposeBuffer();
            SendMessage(model._buffer);
        }

        /// <summary>
        /// Disconnects the client from the server and stops all threads for client.
        /// </summary>
        public void DisconnectClient()
        {
            //Console Message.
            Console.WriteLine("Disconnecting: " + Me.IpAddress + " Name: " + Me.Name);

            //Removes client from server.
            ParentServer.Clients.Remove(this);

            //Call Handlers for disconect user
            BaseRequestModel model = new BaseRequestModel
            {
                Flag = (ushort)RequestFlag.Disconnect
            };

            _dispatcher.Handle(model, Me, this);

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
            Console.WriteLine(Me.IpAddress + " disconnected.");
            Console.WriteLine(Convert.ToString(ParentServer.Clients.Count) + " clients online.");
            AbortThread.Abort();
        }

        /// <summary>
        /// Writes data to the client in sequence on the server.
        /// </summary>
        private void Write(TcpClient client)
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

                        /*Logger.Debug("buffer pre enc");
                        foreach (var a in buffer.Memory)
                            Console.WriteLine(a);*/

                        var encrypted = ParentServer.EncryptBuffer(buffer);

                        /*Logger.Debug("buffer after enc");
                        foreach (var a in encrypted)
                            Console.WriteLine(a);*/
                        Logger.Debug($"LEN: {encrypted.Length}");

                        stream.Write(encrypted, 0, encrypted.Length);
                        stream.Flush();
                    }
                    catch (System.IO.IOException ex)
                    {
                        Logger.Error(ex);
                        DisconnectClient();
                        break;
                    }
                    catch (NullReferenceException ex)
                    {
                        Logger.Error(ex);
                        DisconnectClient();
                        break;
                    }
                    catch (ObjectDisposedException ex)
                    {
                        Logger.Error(ex);
                        break;
                    }
                    catch (System.InvalidOperationException ex)
                    {
                        Logger.Error(ex);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        DisconnectClient();
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
                    NetworkStream stream = client.GetStream();

                    BufferStream readBuffer = new BufferStream(BufferType.BufferSize, BufferType.BufferAlignment);
                    stream.Read(readBuffer.Memory, 0, (int)BufferType.BufferSize);

                    //var decrypted = ParentServer.DecryptBuffer(readBuffer);

                    //readBuffer.ReassignMemory(decrypted);

                    //Read the header data.
                    BaseRequestModel model = BaseRequestModel.FromStream(readBuffer);
                    model.ParseFlag();

                    _dispatcher.Handle(model, Me, this);
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Error(ex);
                    DisconnectClient();
                    break;
                }
                catch (NullReferenceException ex)
                {
                    Logger.Error(ex);
                    DisconnectClient();
                    break;
                }
                catch (ObjectDisposedException ex)
                {
                    Logger.Error(ex);
                    break;
                }
                catch(Exception ex)
                {
                    Logger.Error(ex);
                    DisconnectClient();
                    break;
                }
            }
        }
    }
}
