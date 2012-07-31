using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void EnchantItem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_EnchantItem",packet);
		}
	}
}
