using FluidServer;
using System;
using System.Net.Sockets;

namespace TestSender
{
    class Program
    {
        public static TcpClient client = new TcpClient("10.247.31.69", 6969);
        public static NetworkStream stream;
        static byte[] buffer = new byte[512];
        static void Main(string[] args)
        {
            try
            {
                stream = client.GetStream();
                stream.BeginRead(buffer, 0, 5, new AsyncCallback(OnRead), null);
            }
            catch
            {
                Console.WriteLine("Failed to connect");
            }

            Console.ReadLine();
        }
        static void OnRead(IAsyncResult result)
        {



            stream.EndRead(result);
            byte[] sizeInBytes = new byte[4];
            Buffer.BlockCopy(buffer, 0, sizeInBytes, 0, 4);

            int packetsize = Converter.bytesToInt(sizeInBytes) - 5;
            int packetID = (int)buffer[4];

            if (packetsize == 0)
            {
                if (packetID == 1)
                {
                    Console.WriteLine("Was rejected");
                }
                else if (packetID == 6)
                {
                    Console.WriteLine("Start simulation now!");
                }
            }
            else
            {
                byte[] data;
                stream.Read(buffer, 0, packetsize);
                data = new byte[packetsize];
                Buffer.BlockCopy(buffer, 0, data, 0, packetsize);
                Readhandler.Read(data,packetID);
            }
            stream.BeginRead(buffer, 0, 5, new AsyncCallback(OnRead), null);
        }
    }
}
