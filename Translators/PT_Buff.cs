using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void Buff(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_Buff",packet);
		}
	}
}
