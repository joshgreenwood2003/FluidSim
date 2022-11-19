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
            byte[] dat = {0,1,2,3};
            stream.Write(dat, 0, 4);
        }
        public void disconnect(){
            client.Close();
            client = null;
            connected = false;
        }
    }
}
