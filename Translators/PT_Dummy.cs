using System;

namespace Emulator.Translators
{
    public partial class PacketTranslator
    {
        public static void Dummy(Client client, PacketIn packet)
        {
            // Dummy Translator, for unused packets :-)
        }
    }
}
