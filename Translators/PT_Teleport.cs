using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void Teleport(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_Teleport",packet);
		}
	}
}
