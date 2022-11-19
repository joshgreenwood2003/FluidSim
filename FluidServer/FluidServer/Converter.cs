using System;
using System.Collections.Generic;
using System.Text;

namespace FluidServer
{
    internal class Converter
    {
        public static int bytesToInt(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToInt32(bytes, 0);
        }
        public static byte[] intToBytes(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return bytes;
        }

    }
}
