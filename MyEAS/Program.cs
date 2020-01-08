using MyAES.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEAS
{
    class Program
    {
        static void Main(string[] args)
        {
            string salt = "aselrias38490a32";
            string vector = "8947az34awl34kjq";

            string pass = "abcde";

            Encoding utf8 = Encoding.UTF8;

            var a = new MyAesCrypto(pass, salt, vector);

            byte[] data = utf8.GetBytes("hello world".ToArray());

            Console.WriteLine(utf8.GetString(data, 0, data.Count()));

            foreach (var ch in data)
                Console.Write(ch + " ");

            Console.WriteLine();

            var encrypted = a.Encrypt(data);

            foreach (var ch in encrypted)
                Console.Write(ch + " ");

            Console.WriteLine();

            var decrypted = a.Decrypt(encrypted);

            foreach (var ch in decrypted)
                Console.Write(ch + " ");

            Console.WriteLine();

            Console.WriteLine(utf8.GetString(decrypted, 0, decrypted.Count()));
            Console.ReadLine();
        }
    }
}
