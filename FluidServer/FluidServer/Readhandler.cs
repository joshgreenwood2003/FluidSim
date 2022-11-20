using FluidServer;
using System;

namespace TestSender
{
    internal class Readhandler
    {
        public static void Read(byte[] data, int packetID,int playerID)
        {
            Console.WriteLine(packetID);

            if (packetID == 3)
            {
                byte[] x1b = new byte[4];
                byte[] y1b = new byte[4];
                byte[] x2b = new byte[4];
                byte[] y2b = new byte[4];
                Buffer.BlockCopy(data, 0, x1b, 0, 4);
                Buffer.BlockCopy(data, 4, y1b, 0, 4);
                Buffer.BlockCopy(data, 8, x2b, 0, 4);
                Buffer.BlockCopy(data, 12, y2b, 0, 4);
                float x1 = Converter.bytesToFloat(x1b);
                float y1 = Converter.bytesToFloat(y2b);
                float x2 = Converter.bytesToFloat(x2b);
                float y2 = Converter.bytesToFloat(y1b);


                Console.WriteLine($"Beam placed by player {playerID} at {x1},{y1} to {x2},{y2} ");
                PacketSender.createPoint(x1, y1, x2, y2,playerID);
            }
            else if(packetID == 4)
            {
                int numballs = Converter.bytesToInt(data);
                Console.WriteLine($"Client sent game is done, a total of {numballs} balls were dropped");
                PacketSender.levelEnd(numballs);
                Server.StartRound();
            }

        }
    }
}
