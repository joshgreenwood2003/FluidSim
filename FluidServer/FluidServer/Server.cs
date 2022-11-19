﻿using System;
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
            bool success = false;
            for(int i = 0; i < 2; i++)
            {
                if (clients[i].connected == false)
                {
                    clients[i].connect(client);
                    success = true;
                    break;
                }
            }
            if (success)
            {
                Console.WriteLine("Client connected");
                NetworkStream stream = client.GetStream();
                byte[] dat = new byte[4];
                dat[0] = 0;
                dat[1] = 1;
                dat[2] = 2;
                dat[3] = 3;
                stream.Write(dat, 0, 4);
            }
            else
            {
                Console.WriteLine("Full");
                NetworkStream stream = client.GetStream();
                byte[] dat = new byte[4];
                dat[0] = 0;
                dat[1] = 1;
                dat[2] = 2;
                dat[3] = 3;
                stream.Write(dat, 0, 4);
            }

            listener.BeginAcceptTcpClient(new AsyncCallback(onConnect), null);
        }
        static void init()
        {
            clients[0] = new Client(0);
            clients[1] = new Client(1);
        }
    }
}
