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
        public static Client[] clients = new Client[2];
        NetworkStream stream;
        
        public static void Start(int port)
        {
            init();
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine("Server started at {0}", port);
            listener.BeginAcceptTcpClient(new AsyncCallback(onConnect), null);
        }
        static void onConnect(IAsyncResult result)
        {
            TcpClient client = listener.EndAcceptTcpClient(result);
            listener.BeginAcceptTcpClient(new AsyncCallback(onConnect), null);
            for(int i = 0; i < 2; i++)
            {
                if (clients[i].connected == false)
                {
                    clients[i].connect(client);
                    Console.WriteLine("Client connected");
                    return;
                }
            }
            Console.WriteLine("Full");


        }
        static void init()
        {
            clients[0] = new Client(0);
            clients[1] = new Client(1);
        }
    }
}
