using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Server...");
            Server server = new Server();
            server.StartServer(10103);
            Console.WriteLine("Server Started!");
        }
    }
}
