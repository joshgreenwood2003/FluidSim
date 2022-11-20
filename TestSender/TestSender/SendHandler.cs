using FluidServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestSender
{
    internal class SendHandler
    {
        public static void playerReady() //when player presses ready button, send to server (between start of game and finished building bridge)
        {
            byte[] data = {0,0,0,5,5};
            Program.SendData(data);
        }
        public static void levelEnd(int numBalls)//once the sim has settled, count the number of particles in bucket region and send to server
        {
            byte[] ballsInBucketBytes = Converter.intToBytes(numBalls);
            byte[] data = { 0, 0, 0, 9, 4, 0, 0, 0, 0 };
            Buffer.BlockCopy(ballsInBucketBytes, 0, data, 5, 4);
            Program.SendData(data);
        }
        public static void sendDraw(double x1, double y1, double x2, double y2) //whenever 
        {
            byte[] x1b = Converter.doubleToBytes(x1);
            byte[] x2b = Converter.doubleToBytes(x2);
            byte[] y1b = Converter.doubleToBytes(y1);
            byte[] y2b = Converter.doubleToBytes(y2);
            byte[] data = { 0, 0, 0, 37, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Buffer.BlockCopy(x1b, 0, data, 5, 8);
            Buffer.BlockCopy(x2b, 0, data, 13, 8);
            Buffer.BlockCopy(y1b, 0, data, 21, 8);
            Buffer.BlockCopy(y2b, 0, data, 29, 8);
            Program.SendData(data);
        }
    }
}
