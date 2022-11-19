using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace FluidServer
{
    class Server
    {
        public static TcpListener listener;
        public static Client[] clients;
        public static void Start(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(onConnect), null);
            Console.WriteLine("Server started at {0}", port);
        }
        static void onConnect(IAsyncResult result)
        {
            TcpClient client = listener.EndAcceptTcpClient(result);
            Console.WriteLine("Client connected");
            listener.BeginAcceptTcpClient(new AsyncCallback(onConnect), null);
        }
        static void init()
        {
            clients[0] = new Client(0);
            clients[1] = new Client(1);
        }
    }
}
