using FluidServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestSender
{
    internal class SendHandler
    {
        public static void playerReady()
        {
            byte[] data = {0,0,0,5,5};
            Program.SendData(data);
        }
        public static void levelEnd(int numBalls)
        {
            byte[] ballsInBucketBytes = Converter.intToBytes(numBalls);
            byte[] data = { 0, 0, 0, 9, 4, 0, 0, 0, 0 };
            Buffer.BlockCopy(ballsInBucketBytes, 0, data, 5, 4);
            Program.SendData(data);
        }
        public static void sendDraw(int x1,int y1, int x2, int y2)
        {
            byte[] x1b = Converter.intToBytes(x1);
            byte[] x2b = Converter.intToBytes(x2);
            byte[] y1b = Converter.intToBytes(y1);
            byte[] y2b = Converter.intToBytes(y2);
            byte[] data = { 0, 0, 0, 21, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Buffer.BlockCopy(x1b, 0, data, 5, 4);
            Buffer.BlockCopy(x2b, 0, data, 9, 4);
            Buffer.BlockCopy(y1b, 0, data, 13, 4);
            Buffer.BlockCopy(y2b, 0, data, 17, 4);
            Program.SendData(data);
        }
    }
}
