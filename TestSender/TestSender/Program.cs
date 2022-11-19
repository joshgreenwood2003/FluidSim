using System;
using System.Net.Sockets;
using System.Net;

namespace TestSender
{
    class Program
    {
        public static TcpClient client = new TcpClient("10.247.31.69", 6969);
        public static NetworkStream stream;
        static byte[] buffer = new byte[4];
        static void Main(string[] args)
        {


            stream = client.GetStream();

            stream.BeginRead(buffer, 0, 4, new AsyncCallback(OnRead), null);
            Console.ReadLine();

        }
        static void OnRead(IAsyncResult result)
        {
            Console.WriteLine(buffer[1]);
        }
    }
}
