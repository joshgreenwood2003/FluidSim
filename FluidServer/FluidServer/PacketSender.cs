using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FluidServer
{
    internal class PacketSender
    {

        public static void sendAcceptPacket(Client client,int id)
        {
            byte[] data = { 0, 0, 0, 6, 0, (byte)id};
            Console.WriteLine("Send accept packet");
            client.sendinfo(data);
        }
        public static void sendLevelInfo(int levelID)
        {
            byte[] lvlid = Converter.intToBytes(levelID);
            byte[] data = {0, 0, 0, 9, 2,0,0,0,0};
            Buffer.BlockCopy(lvlid, 0, data, 5, 4);
            Console.WriteLine("Send level info");
            Server.sendToAll(data);
        }
        public static void createPoint(float x1, float y1,float x2,float y2, int idExclude)
        {
            byte[] x1b = Converter.floatToBytes(x1);
            byte[] y1b = Converter.floatToBytes(y1);
            byte[] x2b = Converter.floatToBytes(x2);
            byte[] y2b = Converter.floatToBytes(y2);
            byte[] data = { 0, 0, 0, 21, 3, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0};
            Buffer.BlockCopy(x1b, 0, data, 5, 4);
            Buffer.BlockCopy(y1b, 0, data, 9, 4);
            Buffer.BlockCopy(x2b, 0, data, 13, 4);
            Buffer.BlockCopy(y2b, 0, data, 17, 4);
            Console.WriteLine("Send point data");
            Server.sendToAllExcept(idExclude,data);
        }
        public static void levelEnd(int ballsInBucket)
        {
            byte[] ballsInBucketBytes = Converter.intToBytes(ballsInBucket);
            byte[] data = {0,0,0,9,4,0,0,0,0};
            Buffer.BlockCopy(ballsInBucketBytes, 0, data, 5, 4);
            Console.WriteLine("Send level end");
            Server.sendToAll(data);
        }
        public static void replicateReady(int idExclude)
        {
            byte[] data = { 0, 0, 0, 5, 5 };
            Console.WriteLine("Send replicate the ready");
            Server.sendToAllExcept(idExclude, data);
        }
        public static void startSim()
        {
            Console.WriteLine("Start sim");
            byte[] data = { 0, 0, 0, 5, 6 };
            Server.sendToAll(data);
        }

    }
}
