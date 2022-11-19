﻿using FluidServer;
using System;

namespace TestSender
{
    internal class Readhandler
    {
        public static void Read(byte[] data, int packetID)
        {
            Console.WriteLine(packetID);
            if (packetID == 0)
            {
                Console.WriteLine($"Was accepted with id {(int)data[0]}");
            }
            else if (packetID == 2)
            {
                Console.WriteLine($"Sent game data, draw level number {(int)data[0]}");
            }
            else if (packetID == 3)
            {
                byte[] x1b = new byte[4];
                byte[] x2b = new byte[4];
                byte[] y1b = new byte[4];
                byte[] y2b = new byte[4];
                Buffer.BlockCopy(data, 0, x1b, 0,4);
                Buffer.BlockCopy(data, 4, x2b, 0, 4);
                Buffer.BlockCopy(data, 8, y1b, 0, 4);
                Buffer.BlockCopy(data, 12, y2b, 0, 4);
                int x1 = Converter.bytesToInt(x1b);
                int x2 = Converter.bytesToInt(x2b); 
                int y1 = Converter.bytesToInt(y2b);
                int y2 = Converter.bytesToInt(y1b);
                Console.WriteLine($"Beam placed by other player at {x1},{y1} to {x2},{y2} ");
            }
            else if (packetID == 4)
            {
                int numballs = Converter.bytesToInt(data);
                Console.WriteLine($"Game won, a total of {numballs} balls were dropped");
            }
        }
    }
}