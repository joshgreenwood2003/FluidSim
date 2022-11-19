using System;
using System.Net;
using System.Net.Sockets;
namespace FluidServer
{
    class Program
    {
        const int port = 6969;
        static void Main(string[] args)
        {
            Console.Title = "Game Server";
            Console.WriteLine("Starting server on port {0}", port);
            Server.Start(6969);
        }
    }
}
