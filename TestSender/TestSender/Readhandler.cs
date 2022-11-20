using FluidServer;
using System;

namespace TestSender
{
    internal class Readhandler
    {
        public static void Read(byte[] data, int packetID)
        {
            if (packetID == 0)
            {
                Console.WriteLine($"Was accepted with id {(int)data[0]}");
            }
            else if (packetID == 2)
            {
                int lvlnum = Converter.bytesToInt(data);

                Console.WriteLine($"Game loaded, draw level number {lvlnum}"); //FIND SPECIFIC LEVEL AND LOAD, RESET
            }
            else if (packetID == 3)
            {
                byte[] x1b = new byte[8];
                byte[] x2b = new byte[8];
                byte[] y1b = new byte[8];
                byte[] y2b = new byte[8];
                Buffer.BlockCopy(data, 0, x1b, 0, 8);
                Buffer.BlockCopy(data, 8, x2b, 0, 8);
                Buffer.BlockCopy(data, 16, y1b, 0, 8);
                Buffer.BlockCopy(data, 24, y2b, 0, 8);
                double x1 = Converter.bytesToDouble(x1b);
                double x2 = Converter.bytesToDouble(x2b);
                double y1 = Converter.bytesToDouble(y2b);
                double y2 = Converter.bytesToDouble(y1b);
                Console.WriteLine($"Beam placed by other player at {x1},{y1} to {x2},{y2} "); //ADD BEAM BETWEEN LOCATIONS
            }
            else if (packetID == 4)
            {
                int numballs = Converter.bytesToInt(data);
                Console.WriteLine($"Game won, a total of {numballs} balls were dropped"); //DISPLAY NUMBER OF BALLS DROPPED, RESET
            }
        }
    }
}
