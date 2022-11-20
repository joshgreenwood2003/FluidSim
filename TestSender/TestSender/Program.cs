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
            while (true)
            {
                Console.WriteLine("Send a packet(enter first)");
                Console.ReadLine();
                int packetnum = Convert.ToInt32(Console.ReadLine());
                if (packetnum == 3) //draw beam
                {
                    Console.WriteLine("Write x1");
                    int x1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Write y1");
                    int y1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Write x2");
                    int x2 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Write y2");
                    int y2 = Convert.ToInt32(Console.ReadLine());
                    SendHandler.sendDraw(x1, y1, x2, y2);
                }
                else if (packetnum == 4) //level end, send num of balls
                {
                    Console.WriteLine("Write num of balls");
                    int num = Convert.ToInt32(Console.ReadLine());
                    SendHandler.levelEnd(num);
                }
                else if (packetnum == 5)//player ready
                {
                    SendHandler.playerReady();
                }
            }
            
            Console.ReadLine();
        }
        static void OnRead(IAsyncResult result)
        {

            int len = stream.EndRead(result);
            if (len <= 0)
            {
                return;
            }
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
        public static void SendData(byte[] data)
        {
            for(int i = 0;i< data.Length; i++)
            {
                Console.WriteLine(data[i]);
            }
            stream.Write(data,0,data.Length);
        }
  
    }
}
