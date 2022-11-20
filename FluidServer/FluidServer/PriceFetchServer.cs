using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
namespace FluidServer
{

    internal class PriceFetchServer
    {
        static TcpListener listener = new TcpListener(IPAddress.Any, 6970);
        static TcpClient client;
        static NetworkStream stream;
        static byte[] buffer = new byte[40];
        public static void startServer()
        {
            listener.Start();
            client = listener.AcceptTcpClient();
            stream = client.GetStream();
            stream.BeginRead(buffer,0,40,new AsyncCallback(onRead),null);
        }
        public static float bitcoinvalue;
        public static  void onRead(IAsyncResult result)
        {
            stream.EndRead(result);
          //  for(int i = 0; i < buffer.Length; i++)
            //{
              //  Console.WriteLine(buffer[i]);
            //}
            //Console.ReadLine();
            float[] floats = new float[10];
            byte[] tempfloat = new byte[4];
            for(int i = 0; i < 10; i++)
            {
                Buffer.BlockCopy(buffer, i * 4, tempfloat, 0, 4);
                floats[i] = Converter.bytesToFloat(tempfloat);
            }
            bitcoinvalue = floats[0];

        }
    }
}
