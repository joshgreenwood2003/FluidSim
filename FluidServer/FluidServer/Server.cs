using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace FluidServer
{
    class Server
    {
        public static TcpListener listener;
        public static Client[] clients = new Client[2];
        public static int currentConnectedClients = 0;
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
                    currentConnectedClients++;
                    Console.WriteLine("Client connected");  //this method of finding if server is full is fucking stupid
                    if(currentConnectedClients == 2)                               //what is wrong with me?? please fix
                    {
                        PacketSender.sendLevelInfo(201);
                        Console.WriteLine("Now full");
                    }
                    return;
                }
            }
            try
            {
                Console.WriteLine("Connection attempted but full");
                NetworkStream stream = client.GetStream();  
                byte[] data = { 0, 0, 0, 0, 0, 0, 0, 9,1}; //write a connection failed packet
                stream.Write(data,0,9);
            }
            catch
            {
                Console.WriteLine("Full and failed to send packet");
            }

        }
        static void init()
        {
            clients[0] = new Client(0);
            clients[1] = new Client(1);
        }

        public static void sendToAll(byte[] data)
        {
            foreach (Client client in clients)
            {
                client.sendinfo(data);
            }
        }
    }
}
