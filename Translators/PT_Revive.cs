using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void Revive(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_Revive",packet);
		}
	}
}
