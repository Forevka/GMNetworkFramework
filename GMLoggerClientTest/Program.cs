using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            client.Test();
        }
    }


    public class client
    {

        public static void Test()
        {

            try
            {
                TcpClient tcpclnt = new TcpClient();
                tcpclnt.ReceiveBufferSize = 256;
                tcpclnt.SendBufferSize = 256;
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect("127.0.0.1", 10103);
                // use the ipaddress as in the server program

                Console.WriteLine("Connected");
                Console.Write("Enter the string to be transmitted : ");

                Stream stm = tcpclnt.GetStream();

                UTF8Encoding asen = new UTF8Encoding();
                byte[] ba1 = BitConverter.GetBytes(1234);//asen.GetBytes("hello1");
                byte[] ba2 = BitConverter.GetBytes(5678);//asen.GetBytes("hello2");
                byte[] ba3 = BitConverter.GetBytes(9012123);//asen.GetBytes("qweqweqwe");
                Console.WriteLine("Transmitting.....");

                byte[] flag = BitConverter.GetBytes((ushort)2006);

                byte[] word_count = BitConverter.GetBytes((ushort)3);
                //Array.Reverse(flag);

                var full_buf = Combine(flag, word_count, ba1, ba2, ba3);

                stm.Write(full_buf, 0, full_buf.Length);
                //stm.Write(ba, flag.Length - 1, ba.Count());

                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);

                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(bb[i]));

                tcpclnt.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
            Console.ReadLine();
        }

        private static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

    }
}
