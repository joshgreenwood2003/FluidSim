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
        public static bool[] readystatus = new bool[2];
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
                    Console.WriteLine("Client connected"); 
                    if(currentConnectedClients == 2)                               
                    {
                        StartRound();
                        Console.WriteLine("Server is full. Start first round");
                    }
                    return;
                }
            }
            try
            {
                Console.WriteLine("Connection attempted but full");
                NetworkStream stream = client.GetStream();  
                byte[] data = {0, 0, 0, 5,1}; //write a connection failed packet
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
            readystatus[0] = false;
            readystatus[1] = false;
        }

        public static void sendToAll(byte[] data)
        {
            foreach (Client client in clients)
            {
                client.sendinfo(data);
            }
        }
        public static void sendToAllExcept(int id, byte[] data)
        {
            foreach (Client client in clients)
            {
                if (client.id != id)
                {
                    client.sendinfo(data);
                }

            }
        }
        public static void StartRound()
        {
            PacketSender.sendLevelInfo(201);//random level
            readystatus[0] = false;
            readystatus[1] = false;
        }
        public static void SetReadyState(int playerID)
        {

            readystatus[playerID] = true;
            if (readystatus[0] && readystatus[1])
            {
                PacketSender.startSim();
            }
        }
    }
}
