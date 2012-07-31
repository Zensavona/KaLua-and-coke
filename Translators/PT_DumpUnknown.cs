using System;

namespace Emulator.Translators
{
    public partial class PacketTranslator
    {
        public static void DumpUnknown(Client client, PacketIn packet)
        {
            Utils.DumpUnknown(packet);
        }
    }
}
