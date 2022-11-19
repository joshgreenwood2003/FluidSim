using System;
using System.Collections.Generic;
using System.Text;

namespace FluidServer
{
    internal class PacketSender
    {

        public static void sendAcceptPacket(Client client,int id)
        {
            byte[] data = { 0, 0, 0, 0, 0, 0, 0, 10, 0, (byte)id};
            client.sendinfo(data);
        }
        public static void sendLevelInfo(int levelID)
        {
            byte[] data = { 0, 0, 0, 0, 0, 0, 0, 10, 2,(byte)levelID};
            Server.sendToAll(data);
        }

    }
}
