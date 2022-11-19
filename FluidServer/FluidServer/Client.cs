using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace FluidServer
{

    class Client
    {
        public TcpClient client;
        public bool connected;
        public int id;
        public NetworkStream stream;
        public byte[] buffer = new byte[512];
        public Client(int _id)
        {
            id = _id;
            connected = false;
        }
        public void connect(TcpClient _client)
        {
            connected = true;
            client = _client;
            stream = client.GetStream();
            stream.BeginRead(buffer, 0, 8, onRead, null);
            PacketSender.sendAcceptPacket(this, id);
        }
        public void disconnect(){
            Server.currentConnectedClients--;
            Console.WriteLine($"Client {id} disconnected");
            connected = false;
            client.Close();
            client = null;
            stream = null;

        }
        public void onRead(IAsyncResult result) //IMPLEMENT
        {
            try
            {
                stream.EndRead(result);
                Console.WriteLine($"Server recieved data from client {id}");
            }
            catch{
                disconnect();
            }


        }
        public void sendinfo(byte[] data)//Seems to work?
        {
            if (connected)
            {
                stream.Write(data, 0, data.Length);
            }
        }
    }
}
