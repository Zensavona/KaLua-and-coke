using System;
using Emulator.ClassMaps;

namespace Emulator.Translators
{
    public partial class PacketTranslator
    {
        public static void SessionRequest(Client client,PacketIn packet)
        {
            if(packet.ReadByte() == 0) {
                client.Send(new Packets.SessionRequest(),"Session Request");
            }
        }
    }
}
