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
        public static double bytesToDouble(byte[] bytes)
        {
           if (BitConverter.IsLittleEndian)
           {
                Array.Reverse(bytes);
            }
            return BitConverter.ToDouble(bytes, 0);
        }
        public static byte[] doubleToBytes(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return bytes;
        }

        public static float bytesToFloat(byte[] bytes)
        {
           if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToSingle(bytes, 0);
        }
        public static byte[] floatToBytes(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return bytes;
        }

    }
}
