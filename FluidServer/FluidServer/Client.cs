using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TestSender;

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
            stream.BeginRead(buffer, 0, 5, onRead, null);
            PacketSender.sendAcceptPacket(this, id);
        }
        public void disconnect(){
            Server.currentConnectedClients--;
            Console.WriteLine($"Client {id} disconnected");
            connected = false;
            Server.readystatus[id] = false;
            client.Close();
            client = null;
            stream = null;

        }
        public void onRead(IAsyncResult result) //IMPLEMENT
        {
          //  try
           // {
                int len = stream.EndRead(result);
                if (len <= 0)
                {
                    disconnect();
                    return;
                }

                byte[] sizeInBytes = new byte[4];
                Buffer.BlockCopy(buffer, 0, sizeInBytes, 0, 4);

                int packetsize = Converter.bytesToInt(sizeInBytes) - 5;
                int packetID = (int)buffer[4];
                Console.WriteLine(packetID);
                Console.WriteLine(packetsize);

                if (packetsize == 0)
                {
                    if(packetID == 5)
                    {
                        Console.WriteLine($"Player {id} is ready");
                        PacketSender.replicateReady(id);
                         Server.SetReadyState(id);
                    }
                }
                else
                {
                    byte[] data;
                    stream.Read(buffer, 0, packetsize);
                    data = new byte[packetsize];
                    Buffer.BlockCopy(buffer, 0, data, 0, packetsize);
                    Readhandler.Read(data, packetID,id);
                }
                stream.BeginRead(buffer, 0, 5, new AsyncCallback(onRead), null);
          // }
           // catch(Exception e){
            //    Console.WriteLine("AAA?");
          //      Console.WriteLine(e.Message);
           //     disconnect();
           // }


        }
        public void sendinfo(byte[] data)//Seems to work?
        {
            if (connected)
            {
                Console.WriteLine("SENDING SOMETHING");
                for (int i = 0; i < data.Length; i++)
                {
                    Console.Write(data[i]);
                }
                Console.WriteLine("\n");
                try
                {
                    stream.Write(data, 0, data.Length);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Client disconnected");
                    disconnect();
                }

            }
        }
    }
}
