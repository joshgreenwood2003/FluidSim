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
        public Client(int _id)
        {
            id = _id;
            connected = false;
        }
        public void connect(TcpClient _client)
        {
            connected = true;
            client = _client;
            NetworkStream stream = client.GetStream();
            byte[] dat = new byte[4];
            dat[0] = 0;
            dat[1] = 1;
            dat[2] = 2;
            dat[3] = 3;
            stream.Write(dat, 0, 4);
        }
    }
}
