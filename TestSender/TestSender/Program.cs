using System;
using System.Net.Sockets;
using System.Net;

namespace TestSender
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("10.247.31.69", 6969);
            Console.ReadLine();

        }
    }
}
