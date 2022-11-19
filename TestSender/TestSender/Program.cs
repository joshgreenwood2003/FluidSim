using System;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;

namespace TestSender
{
    class Program
    {
        public static TcpClient client = new TcpClient("10.247.31.69", 6969);
        public static NetworkStream stream;
        static byte[] buffer = new byte[512];
        static void Main(string[] args)
        {
            stream = client.GetStream();
            stream.BeginRead(buffer, 0, 9, new AsyncCallback(OnRead), null);
            Console.ReadLine();
        }
        static void OnRead(IAsyncResult result)
        {
            stream.EndRead(result);
            byte[] sizeInBytes = new byte[8];
            Buffer.BlockCopy(buffer, 0, sizeInBytes, 0, 8);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(sizeInBytes);
            }
            int packetsize = BitConverter.ToInt32(sizeInBytes, 0) - 9 ;
            int packetID = (int)buffer[8];
            byte[] data;

            if (packetsize == 0)
            {
                if (packetID == 1)
                {
                    Console.WriteLine("Was rejected");
                }
            }
            else
            {
                stream.Read(buffer, 0, packetsize);

                data = new byte[packetsize];
                Buffer.BlockCopy(buffer, 0, data, 0, packetsize);
                //now have int packetID and bytearray data


                Console.WriteLine(packetID);
                if (packetID == 0)
                {
                    Console.WriteLine($"Was accepted with id {(int)data[0]}");
                }
                else if (packetID == 2)
                {
                    Console.WriteLine($"Sent game data, draw level number {(int)data[0]}");
                }
            }
            stream.BeginRead(buffer, 0, 9, new AsyncCallback(OnRead), null);
        }
    }
}
